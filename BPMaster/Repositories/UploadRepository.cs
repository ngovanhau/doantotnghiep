using System.Data;
using BPMaster.Domains.Dtos;
using BPMaster.Domains.Entities;
using Common.Databases;
using Common.Repositories;
using Dapper;
using Domain.Entities;
using Common.Application.CustomAttributes;
using Common.Services;
using Repositories;
using Utilities;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;


namespace Repositories
{
    [ScopedService]
    public class UploadRepository
    {
        private readonly string _bucketName = "mydb-71221.appspot.com";
        private readonly string _credentialPath = @"C:\TEST\DOAN\doantotnghiep\BPMaster\Firebase\mydb-71221-firebase-adminsdk-gbxif-2368543148.json";

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var storageClient = await StorageClient.CreateAsync(GoogleCredential.FromFile(_credentialPath));

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                await storageClient.UploadObjectAsync(_bucketName, fileName, "image/png", fileStream);
            }

            var storageObject = storageClient.GetObject(_bucketName, fileName);
            storageObject.Acl = new[] { new Google.Apis.Storage.v1.Data.ObjectAccessControl { Entity = "allUsers", Role = "READER" } };
            storageClient.UpdateObject(storageObject);

            var imageUrl = $"https://storage.googleapis.com/{_bucketName}/{fileName}";

            File.Delete(filePath); // Xóa file tạm sau khi upload

            return imageUrl;
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            var fileName = imageUrl.Split(new string[] { $"{_bucketName}/" }, StringSplitOptions.None)[1];

            var storageClient = await StorageClient.CreateAsync(GoogleCredential.FromFile(_credentialPath));

            await storageClient.DeleteObjectAsync(_bucketName, fileName);
        }
    }
}

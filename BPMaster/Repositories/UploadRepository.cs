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
using Google.Cloud.SecretManager.V1;
using System.Text.Json;
using System.Threading.Tasks;
using Google;

namespace Repositories
{
    [ScopedService]
    public class UploadRepository
    {
        private readonly string _bucketName = "uploadimg-97839.appspot.com"; 
        private readonly string _projectId = "upload-441309"; 
        private readonly string _secretId = "uploadimage"; 
        private readonly string _version = "1"; 

        // Lấy Firebase credentials từ Google Secret Manager
        private async Task<string> GetFirebaseCredentialsJsonAsync()
        {
            try
            {
                SecretManagerServiceClient client = await SecretManagerServiceClient.CreateAsync();
                var secretVersionName = new SecretVersionName(_projectId, _secretId, _version);
                AccessSecretVersionResponse result = await client.AccessSecretVersionAsync(secretVersionName);

                // Ghi thông tin khi lấy thành công
                Console.WriteLine("Secret fetched successfully.");
                return result.Payload.Data.ToStringUtf8(); // Trả về giá trị Secret dưới dạng JSON
            }
            catch (GoogleApiException googleEx)
            {
                // Ghi thông tin chi tiết khi gặp lỗi từ Google API
                Console.WriteLine($"Google API Error: {googleEx.Message}");
                throw new ApplicationException("Lỗi khi lấy chứng thực Firebase từ Secret Manager", googleEx);
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi chung
                Console.WriteLine($"Lỗi khi truy cập secret: {ex.Message}");
                throw new ApplicationException("Lỗi khi lấy chứng thực Firebase từ Secret Manager", ex);
            }
        }

        // Tạo StorageClient sử dụng chứng thực từ Secret Manager
        private async Task<StorageClient> CreateStorageClientAsync()
        {
            string firebaseCredentialsJson = await GetFirebaseCredentialsJsonAsync();
            GoogleCredential credential = GoogleCredential.FromJson(firebaseCredentialsJson)
                .CreateScoped(new[] { "https://www.googleapis.com/auth/cloud-platform" });
            return await StorageClient.CreateAsync(credential);
        }

        // Tải ảnh lên Google Cloud Storage
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Không có tệp nào được tải lên.");

            var storageClient = await CreateStorageClientAsync();

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    await storageClient.UploadObjectAsync(_bucketName, fileName, file.ContentType, fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải tệp lên: {ex.Message}");
                throw;
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            try
            {
                // Thiết lập quyền công khai cho file đã upload
                var storageObject = await storageClient.GetObjectAsync(_bucketName, fileName);
                storageObject.Acl = new[]
                {
                    new Google.Apis.Storage.v1.Data.ObjectAccessControl
                    {
                        Entity = "allUsers",
                        Role = "READER"
                    }
                };
                await storageClient.UpdateObjectAsync(storageObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thiết lập quyền truy cập ACL: {ex.Message}");
                throw;
            }

            return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
        }

        // Xóa ảnh khỏi Google Cloud Storage
        public async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                throw new ArgumentException("URL của ảnh là bắt buộc.");

            // Lấy tên file từ URL
            var fileName = imageUrl.Split(new string[] { $"{_bucketName}/" }, StringSplitOptions.None)[1];
            var storageClient = await CreateStorageClientAsync();

            try
            {
                // Xóa file khỏi bucket
                await storageClient.DeleteObjectAsync(_bucketName, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa tệp: {ex.Message}");
                throw;
            }
        }
    }
}

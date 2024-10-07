using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BPMaster.Controllers.v1
{
    public class Upload : ControllerBase
    {
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { success = false, message = "No file uploaded." });
            }

            try
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Path.GetTempPath(), fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var storageClient = await StorageClient.CreateAsync(GoogleCredential.FromFile(@"C:\lsp-workflow-backend\BPMaster\Firebase\mydb-71221-firebase-adminsdk-gbxif-0fd49e35fa.json"));
                var bucketName = "mydb-71221.appspot.com"; 

                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    await storageClient.UploadObjectAsync(bucketName, fileName, "image/png", fileStream);
                }

                var storageObject = storageClient.GetObject(bucketName, fileName);
                storageObject.Acl = new[] { new Google.Apis.Storage.v1.Data.ObjectAccessControl { Entity = "allUsers", Role = "READER" } };
                storageClient.UpdateObject(storageObject);

                var imageUrl = $"https://storage.googleapis.com/{bucketName}/{fileName}";

                return Ok(new { success = true, imageUrl, message = "Uploaded successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}

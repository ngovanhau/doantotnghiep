using Application.Settings;
using BPMaster.Domains.Dtos;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace BPMaster.Controllers.v1
{
    public class UploadController(IServiceProvider service) : BaseV1Controller<uploadService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api upload image
        /// </summary>
       
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            return CreatedSuccess(await _service.UploadImageAsync(imageFile));
        }
        /// <summary>
        /// this is api delete image
        /// </summary>
        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteImage(string imageUrl)
        {
            await _service.DeleteImageAsync(imageUrl);
            return Success("delete Success");
        }
    }
}

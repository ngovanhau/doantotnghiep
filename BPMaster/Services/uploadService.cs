using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;

namespace BPMaster.Services
{
    [ScopedService]
    public class uploadService(IServiceProvider services,
    ApplicationSetting setting,
    IDbConnection connection,
    UploadRepository uploadRepository) : BaseService(services)
    {
        private readonly UploadRepository _uploadRepository = uploadRepository;

        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("No file provided for upload.");
            }

            return await _uploadRepository.UploadImageAsync(imageFile);
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentException("Image URL is required.");
            }

            await _uploadRepository.DeleteImageAsync(imageUrl);
        }
    }

}



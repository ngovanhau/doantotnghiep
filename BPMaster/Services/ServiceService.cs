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
    public class ServiceService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly ServiceRepository _ServiceRepository = new(connection);

        public async Task<List<Service>> GetAllService()
        {
            return await _ServiceRepository.GetAllService();
        }

        public async Task<Service> GetByIDService(Guid ServiceId)
        {
            var Service = await _ServiceRepository.GetByIDService(ServiceId);

            if (Service == null)
            {
                throw new NonAuthenticateException();
            }
            return Service;
        }

        public async Task<Service> CreateServiceAsync(ServiceDto dto)
        {
            var Service = _mapper.Map<Service>(dto);

            Service.Id = Guid.NewGuid();

            await _ServiceRepository.CreateAsync(Service);

            return Service;
        }
        public async Task<Service> UpdateServiceAsync(Guid id, ServiceDto dto)
        {
            var existingService = await _ServiceRepository.GetByIDService(id);

            if (existingService == null)
            {
                throw new Exception("Service not found !");
            }
            var Service = _mapper.Map(dto, existingService);

            await _ServiceRepository.UpdateAsync(Service);

            return Service;
        }
        public async Task DeleteServiceAsync(Guid id)
        {
            var Service = await _ServiceRepository.GetByIDService(id);
            if (Service == null)
            {
                throw new Exception("Service not found !");
            }
            await _ServiceRepository.DeleteAsync(Service);
        }

    }
}



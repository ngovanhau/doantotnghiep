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
    public class ServiceMeterReadingsSevice(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly ServiceMeterReadingsRepository _ServiceRepository = new(connection);

        public async Task<List<ServiceMeterReadings>> GetAll()
        {
            return await _ServiceRepository.GetAll();
        }

        public async Task<ServiceMeterReadings> GetByID(Guid Id)
        {
            var Service = await _ServiceRepository.GetByID(Id);

            if (Service == null)
            {
                throw new NonAuthenticateException();
            }
            return Service;
        }

        public async Task<ServiceMeterReadings> CreateAsync(ServiceMeterReadingsDto dto)
        {
            var Service = _mapper.Map<ServiceMeterReadings>(dto);
            Service.Id = Guid.NewGuid();

            var oldMeterReadings = await _ServiceRepository.GetOldMeterReadings(Service.room_id);
            if (oldMeterReadings != null)
            {
                Service.electricity_old = oldMeterReadings.electricity_new;
                Service.water_old = oldMeterReadings.water_new;
            }

            ValidateReadings(Service); 
            CalculateCosts(Service);

            await _ServiceRepository.CreateAsync(Service);
            return Service;
        }

        public async Task<ServiceMeterReadings> UpdateAsync(Guid id, ServiceMeterReadingsDto dto)
        {
            var existingService = await _ServiceRepository.GetByID(id);
            if (existingService == null)
            {
                throw new Exception("ServiceMeterReadings not found !");
            }
            var Service = _mapper.Map<ServiceMeterReadings>(dto);

            var oldMeterReadings = await _ServiceRepository.GetOldMeterReadings(Service.room_id);
            if (oldMeterReadings != null)
            {
                Service.electricity_old = oldMeterReadings.electricity_new;
                Service.water_old = oldMeterReadings.water_new;
            }

            ValidateReadings(Service); 
            CalculateCosts(Service);

            await _ServiceRepository.UpdateAsync(Service);
            return Service;
        }

        public async Task DeleteAsync(Guid id)
        {
            var Service = await _ServiceRepository.GetByID(id);
            if (Service == null)
            {
                throw new Exception("ServiceMeterReadings not found !");
            }
            await _ServiceRepository.DeleteAsync(Service);
        }

        private void CalculateCosts(ServiceMeterReadings service)
        {
            if (service.electricity_old >= service.electricity_new || service.water_old >= service.water_new)
            {
                throw new Exception("New readings must be greater than old readings.");
            }

            service.electricity_cost = (service.electricity_new - service.electricity_old) * service.electricity_price;
            service.water_cost = (service.water_new - service.water_old) * service.water_price;
            service.total_amount = service.electricity_cost + service.water_cost;
        }

        private void ValidateReadings(ServiceMeterReadings service)
        {
            if (service.electricity_price < 0 || service.water_price < 0)
            {
                throw new Exception("Prices must be non-negative.");
            }

            if (service.electricity_new < 0 || service.electricity_old < 0 || service.water_new < 0 || service.water_old < 0)
            {
                throw new Exception("Readings must be non-negative.");
            }

            if (service.electricity_new < service.electricity_old || service.water_new < service.water_old)
            {
                throw new Exception("New readings must be greater than old readings.");
            }
        }

        // get by roomid 
        public async Task<ServiceMeterReadings> getbyroomid(Guid Id)
        {
            var Service = await _ServiceRepository.GetOldMeterReadings(Id);

            if (Service == null)
            {
                throw new NonAuthenticateException("not found !");
            }
            return Service;
        }
    }
}

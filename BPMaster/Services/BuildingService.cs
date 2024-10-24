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
    public class BuildingService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly BuildingRepository _buildingRepository = new(connection);

        public async Task<List<BuildingDto>> GetAllBuildings()
        {
            var buildings = await _buildingRepository.GetAllBuilding();
            var result = new List<BuildingDto>();

            foreach (var building in buildings)
            {
                var feeBasedServices = await _buildingRepository.GetFeeBasedServicesByBuilding(building.Id);
                var freeServices = await _buildingRepository.GetFreeServicesByBuilding(building.Id);

                var dto = _mapper.Map<BuildingDto>(building);

                dto.fee_based_service = feeBasedServices; 
                dto.free_service = freeServices;

                result.Add(dto);
            }

            return result;
        }

        public async Task<BuildingDto> GetByIDBuilding(Guid buildingId)
        {
            var building = await _buildingRepository.GetByIDBuilding(buildingId);

            if (building == null)
            {
                throw new NonAuthenticateException();
            }

            var feeBasedServices = await _buildingRepository.GetFeeBasedServicesByBuilding(buildingId);
            var freeServices = await _buildingRepository.GetFreeServicesByBuilding(buildingId);

            var dto = _mapper.Map<BuildingDto>(building);

            dto.fee_based_service = feeBasedServices; 

            dto.free_service = freeServices;

            return dto;
        }

        public async Task<Building> CreateBuildingAsync(BuildingDto dto)
        {
            var building = _mapper.Map<Building>(dto);
            building.Id = Guid.NewGuid();

            await _buildingRepository.CreateAsync(building);

            if (dto.fee_based_service != null && dto.fee_based_service.Count > 0)
            {
                var serviceIds = dto.fee_based_service.Select(s => s.ServiceId).ToList(); 
                await _buildingRepository.AddServicesToBuilding(building.Id, serviceIds);
            }

            if (dto.free_service != null && dto.free_service.Count > 0) 
            {
                var serviceFreeIds = dto.free_service.Select(s =>s.ServiceId).ToList();
                await _buildingRepository.AddServicesFreeBuilding(building.Id, serviceFreeIds);
            }

            return building;
        }
        public async Task<Building> UpdateBuildingAsync(Guid id, BuildingDto dto)
        {
            var existingBuilding = await _buildingRepository.GetByIDBuilding(id);

            if (existingBuilding == null)
            {
                throw new Exception("Building not found");
            }

            var building = _mapper.Map(dto, existingBuilding);

            await _buildingRepository.UpdateAsync(building);

            await _buildingRepository.RemoveServicesFromBuilding(id);
            await _buildingRepository.RemoveServicesFreeFromBuilding(id);

            if (dto.fee_based_service != null && dto.fee_based_service.Count > 0)
            {
                var serviceIds = dto.fee_based_service.Select(s => s.ServiceId).ToList(); 
                await _buildingRepository.AddServicesToBuilding(id, serviceIds);
            }
            if (dto.free_service != null && dto.free_service.Count > 0)
            {
                var serviceFreeIds = dto.free_service.Select(s => s.ServiceId).ToList();
                await _buildingRepository.AddServicesFreeBuilding(building.Id, serviceFreeIds);
            }

            return building;
        }
        public async Task DeleteBuildingAsync(Guid id)
        {
            var building = await _buildingRepository.GetByIDBuilding(id);

            if (building == null)
            {
                throw new Exception("Building not found");
            }

            await _buildingRepository.RemoveServicesFreeFromBuilding(id);
            await _buildingRepository.RemoveServicesFromBuilding(id);
            await _buildingRepository.DeleteAsync(building);
        }

    }
}

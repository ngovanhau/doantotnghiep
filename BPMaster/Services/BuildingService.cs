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

        public async Task<List<Building>> GetAllBuilding()
        {
            return await _buildingRepository.GetAllBuilding();
        }

        public async Task<Building> GetByIDBuilding(Guid BuildingId)
        {
            var Building = await _buildingRepository.GetByIDBuilding(BuildingId);

            if (Building == null)
            {
                throw new NonAuthenticateException();
            }
            return Building;
        }

        public async Task<Building> CreateBuildingAsync(BuildingDto dto)
        {
            var building = _mapper.Map<Building>(dto);

            building.Id = Guid.NewGuid();

            await _buildingRepository.CreateAsync(building);

            return building;
        }
        public async Task<Building> UpdateBuildingAsync(Guid id, BuildingDto dto)
        {
            var existingBuilding = await _buildingRepository.GetByIDBuilding(id);

            if (existingBuilding == null)
            {
                throw new Exception("Error");
            }
            var Building = _mapper.Map(dto, existingBuilding);

            await _buildingRepository.UpdateAsync(Building);

            return Building;
        }
        public async Task DeleteBuildingAsync(Guid id)
        {
            var Building = await _buildingRepository.GetByIDBuilding(id);
            if (Building == null)
            {
                throw new Exception("product not found !");
            }
            await _buildingRepository.DeleteAsync(Building);
        }
        
    }
}

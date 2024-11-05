
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


namespace Repositories
{
    [ScopedService]
    public class BuildingRepository(IDbConnection connection) : SimpleCrudRepository<Building, Guid>(connection)
    {
        public async Task<List<Building>> GetAllBuilding()
        {
            var sql = SqlCommandHelper.GetSelectSql<Building>();
            var result = await connection.QueryAsync<Building>(sql);
            return result.ToList();
        }
        public async Task<Building?> GetByIDBuilding(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Building>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Building> UpdateBuilding(Building building)
        {
            return await UpdateAsync(building);
        }
        public async Task DeleteBuilding(Building building)
        {
            await DeleteAsync(building);
        }

        public async Task AddServicesToBuilding(Guid buildingId, List<Guid> serviceIds)
        {
            foreach (var serviceId in serviceIds)
            {
                var entry = new { BuildingId = buildingId, ServiceId = serviceId };
                var sql = "INSERT INTO buildingfeebasedservice (BuildingId, ServiceId) VALUES (@BuildingId, @ServiceId)";
                await connection.ExecuteAsync(sql, entry);
            }
        }
        public async Task RemoveServicesFromBuilding(Guid buildingId)
        {
            var sql = "DELETE FROM buildingfeebasedservice WHERE BuildingId = @BuildingId";
            await connection.ExecuteAsync(sql, new { BuildingId = buildingId });
        }
        public async Task<List<BuildingFeeBaseServiceDto>> GetFeeBasedServicesByBuilding(Guid buildingId)
        {
            var sql = @"SELECT s.""Id"" as ServiceId, s.""service_name"" as ServiceName
                FROM buildingfeebasedservice bfs
                INNER JOIN service s ON bfs.""serviceid"" = s.""Id""
                WHERE bfs.""buildingid"" = @BuildingId";
            var result = await connection.QueryAsync<BuildingFeeBaseServiceDto>(sql, new { BuildingId = buildingId });
            return result.ToList();
        }

        public async Task AddServicesFreeBuilding(Guid buildingId, List<Guid> serviceIds)
        {
            foreach (var serviceId in serviceIds)
            {
                var entry = new { BuildingId = buildingId, ServiceId = serviceId };
                var sql = "INSERT INTO buildingfreeservice (BuildingId, ServiceId) VALUES (@BuildingId, @ServiceId)";
                await connection.ExecuteAsync(sql, entry);
            }
        }
        public async Task RemoveServicesFreeFromBuilding(Guid buildingId)
        {
            var sql = "DELETE FROM buildingfreeservice WHERE BuildingId = @BuildingId";
            await connection.ExecuteAsync(sql, new { BuildingId = buildingId });
        }
        public async Task<List<BuildingFreeServiceDto>> GetFreeServicesByBuilding(Guid buildingId)
        {
            var sql = @"SELECT s.""Id"" as ServiceId, s.""service_name"" as ServiceName
                FROM buildingfreeservice bfs
                INNER JOIN service s ON bfs.""serviceid"" = s.""Id""
                WHERE bfs.""buildingid"" = @BuildingId";
            var result = await connection.QueryAsync<BuildingFreeServiceDto>(sql, new { BuildingId = buildingId });
            return result.ToList();
        }
        public async Task<List<Building>> GetBuildingsByUserId(Guid userId)
        {
            var sqlPermission = @"SELECT ""BuildingId"" FROM permission WHERE ""UserId"" = @UserId";
            var buildingIds = await connection.QueryAsync<Guid>(sqlPermission, new { UserId = userId });

            if (!buildingIds.Any())
            {
                return new List<Building>();
            }

            var sqlBuilding = @"SELECT * FROM ""building"" WHERE ""Id"" = ANY(@BuildingIds::uuid[])";
            var buildings = await connection.QueryAsync<Building>(sqlBuilding, new { BuildingIds = buildingIds.ToArray() });

            return buildings.ToList();
        }
    }
}


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
    }
}

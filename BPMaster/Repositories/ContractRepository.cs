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
    public class ContractRepository(IDbConnection connection) : SimpleCrudRepository<Contract, Guid>(connection)
    {
        public async Task<List<Contract>> GetAllContract()
        {
            var sql = SqlCommandHelper.GetSelectSql<Contract>();
            var result = await connection.QueryAsync<Contract>(sql);
            return result.ToList();
        }
        public async Task<Contract?> GetByIDContract(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Contract>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Contract> UpdateContract(Contract Contract)
        {
            return await UpdateAsync(Contract);
        }
        public async Task DeleteContract(Contract Contract)
        {
            await DeleteAsync(Contract);
        }
        public async Task<List<Contract>> GetByBuildingId(Guid buildingId)
        {
            var sql = @"
            SELECT c.*
            FROM contract c
            INNER JOIN room r ON c.""roomId"" = r.""Id""
            WHERE r.""Building_Id"" = @BuildingId;";

            var result = await connection.QueryAsync<Contract>(sql, new { BuildingId = buildingId });
            return result.ToList();
        }
    }
}

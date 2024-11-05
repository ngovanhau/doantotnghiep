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
    public class PermisionManagementRepository(IDbConnection connection) : SimpleCrudRepository<PermissionManagement, Guid>(connection)
    {
        public async Task<List<PermissionManagement>> GetAll()
        {
            var sql = SqlCommandHelper.GetSelectSql<PermissionManagement>();
            var result = await connection.QueryAsync<PermissionManagement>(sql);
            return result.ToList();
        }
        public async Task<PermissionManagement?> GetByID(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<PermissionManagement>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<PermissionManagement> Update(PermissionManagement Deposit)
        {
            return await UpdateAsync(Deposit);
        }
        public async Task Delete(PermissionManagement Deposit)
        {
            await DeleteAsync(Deposit);
        }
        public async Task<List<PermissionManagement>> GetUserByBuildingId(Guid id)
        {
            var param = new { BuildingId = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<PermissionManagement>(new { BuildingId = id });
            var result = await connection.QueryAsync<PermissionManagement>(sql, param);
            return result.ToList();
        }
    }
}


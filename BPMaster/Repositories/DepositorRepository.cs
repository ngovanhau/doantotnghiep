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
    public class DepositorRepository(IDbConnection connection) : SimpleCrudRepository<Depositor, Guid>(connection)
    {
        public async Task<List<Depositor>> GetAllDepositor()
        {
            var sql = SqlCommandHelper.GetSelectSql<Depositor>();
            var result = await connection.QueryAsync<Depositor>(sql);
            return result.ToList();
        }
        public async Task<Depositor?> GetByIDDepositor(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Depositor>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Depositor> UpdateDepositor(Depositor Depositor)
        {
            return await UpdateAsync(Depositor);
        }
        public async Task DeleteDepositor(Depositor Depositor)
        {
            await DeleteAsync(Depositor);
        }
    }
}



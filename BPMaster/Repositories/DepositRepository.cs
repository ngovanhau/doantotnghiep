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
    public class DepositRepository(IDbConnection connection) : SimpleCrudRepository<Deposit, Guid>(connection)
    {
        public async Task<List<Deposit>> GetAllDeposit()
        {
            var sql = SqlCommandHelper.GetSelectSql<Deposit>();
            var result = await connection.QueryAsync<Deposit>(sql);
            return result.ToList();
        }
        public async Task<Deposit?> GetByIDDeposit(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Deposit>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Deposit> UpdateDeposit(Deposit Deposit)
        {
            return await UpdateAsync(Deposit);
        }
        public async Task DeleteDeposit(Deposit Deposit)
        {
            await DeleteAsync(Deposit);
        }
    }
}


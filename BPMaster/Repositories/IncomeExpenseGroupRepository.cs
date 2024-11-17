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
    public class IncomeExpenseGroupRepository(IDbConnection connection) : SimpleCrudRepository<IncomeExpenseGroup, Guid>(connection)
    {
        public async Task<List<IncomeExpenseGroup>> GetAll()
        {
            var sql = SqlCommandHelper.GetSelectSql<IncomeExpenseGroup>();
            var result = await connection.QueryAsync<IncomeExpenseGroup>(sql);
            return result.ToList();
        }
        public async Task<IncomeExpenseGroup?> GetByID(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<IncomeExpenseGroup>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<IncomeExpenseGroup> Update(IncomeExpenseGroup obj)
        {
            return await UpdateAsync(obj);
        }
        public async Task Delete(IncomeExpenseGroup obj)
        {
            await DeleteAsync(obj);
        }
    }
}

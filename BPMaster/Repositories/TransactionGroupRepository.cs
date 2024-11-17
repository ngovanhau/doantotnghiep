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
    public class TransactionGroupRepository(IDbConnection connection) : SimpleCrudRepository<TransactionGroup, Guid>(connection)
    {
        public async Task<List<TransactionGroup>> GetAll()
        {
            var sql = SqlCommandHelper.GetSelectSql<TransactionGroup>();
            var result = await connection.QueryAsync<TransactionGroup>(sql);
            return result.ToList();
        }
        public async Task<TransactionGroup?> GetByID(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<TransactionGroup>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<TransactionGroup> UpdateTransactionGroup(TransactionGroup obj)
        {
            return await UpdateAsync(obj);
        }
        public async Task Delete(TransactionGroup obj)
        {
            await DeleteAsync(obj);
        }
        public async Task<List<TransactionGroup>> GetByType(int type)
        {
            var sql = "SELECT * FROM transactiongroup WHERE type = @Id";
            var result = await connection.QueryAsync<TransactionGroup>(sql, new { Id = type });
            return result.ToList();
        }
    }
}
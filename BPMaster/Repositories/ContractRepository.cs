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
    }
}

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
    public class BillRepository(IDbConnection connection) : SimpleCrudRepository<Bill, Guid>(connection)
    {
        public async Task<List<Bill>> GetAll()
        {
            var sql = SqlCommandHelper.GetSelectSql<Bill>();
            var result = await connection.QueryAsync<Bill>(sql);
            return result.ToList();
        }
        public async Task<Bill?> GetByID(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Bill>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Bill> Updatebill(Bill bill)
        {
            return await UpdateAsync(bill);
        }
        public async Task Delete(Bill bill)
        {
            await DeleteAsync(bill);
        }
    }
}
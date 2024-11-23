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
        public async Task<List<Bill>> GetBillByBuildingId(Guid id)
        {
            var sql = "SELECT * FROM bill WHERE building_id = @Id";
            var result = await connection.QueryAsync<Bill>(sql, new { Id = id });
            return result.ToList();
        }
        public async Task<List<Bill>> GetBillByRoomId(Guid id)
        {
            var sql = "SELECT * FROM bill WHERE roomid = @Id";
            var result = await connection.QueryAsync<Bill>(sql, new { Id = id });
            return result.ToList();
        }

        //thanh toán vn pay

        public async Task<Bill?> GetByTransactionId(string transactionId)
        {
            var sql = "SELECT * FROM Bill WHERE transaction_id = @transactionId";
            return await GetOneByConditionAsync(sql, new { transactionId });
        }

        public async Task UpdateTransactionStatus(string transactionId, int paymentStatus, DateTime? paymentDate = null)
        {
            var sql = @"
            UPDATE Bill
            SET status_payment = @paymentStatus,
                payment_date = @paymentDate
            WHERE transaction_id = @transactionId";

            await connection.ExecuteAsync(sql, new { transactionId, paymentStatus, paymentDate });
        }

    }
}
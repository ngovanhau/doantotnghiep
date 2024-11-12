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
    public class ServiceMeterReadingsRepository(IDbConnection connection) : SimpleCrudRepository<ServiceMeterReadings, Guid>(connection)
    {
        public async Task<List<ServiceMeterReadings>> GetAll()
        {
            var sql = SqlCommandHelper.GetSelectSql<ServiceMeterReadings>();
            var result = await connection.QueryAsync<ServiceMeterReadings>(sql);
            return result.ToList();
        }
        public async Task<ServiceMeterReadings?> GetByID(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<ServiceMeterReadings>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<ServiceMeterReadings> UpdateAsyncRP(ServiceMeterReadings Service)
        {
            return await UpdateAsync(Service);
        }
        public async Task DeleteAsyncRP(ServiceMeterReadings Service)
        {
            await DeleteAsync(Service);
        }
        public async Task<ServiceMeterReadings?> GetOldMeterReadings(Guid roomId)
        {
            var sql = @"
                SELECT * 
                FROM servicemeterreadings
                WHERE room_id = @RoomId
                ORDER BY record_date DESC
                LIMIT 1;";  

            var param = new { RoomId = roomId };
            var result = await connection.QueryFirstOrDefaultAsync<ServiceMeterReadings>(sql, param);
            return result;
        }
    }
}

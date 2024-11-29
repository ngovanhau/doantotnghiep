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
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;


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
            ORDER BY ""CreatedAt"" DESC
            LIMIT 1;";

            var param = new { RoomId = roomId };
            var result = await connection.QueryFirstOrDefaultAsync<ServiceMeterReadings>(sql, param);
            return result;
        }
        public async Task<List<ServiceMeterReadings>> getlistbybuildingid(Guid Id)
        {
            var sql = "SELECT * FROM servicemeterreadings WHERE building_id = @bdId";
            var result = await connection.QueryAsync<ServiceMeterReadings>(sql, new { bdId = Id });
            return result.ToList();
        }
        public async Task<List<ServiceMeterReadings>> getlistbyroomid(Guid Id)
        {
            var sql = "SELECT * FROM servicemeterreadings WHERE room_id = @RId";
            var result = await connection.QueryAsync<ServiceMeterReadings>(sql, new { RId = Id });
            return result.ToList();
        }
        public async Task updatestatus(Guid id, int status)
        {
            var sql = "UPDATE servicemeterreadings  SET status = @Status WHERE \"Id\"= @Id";
            await connection.ExecuteAsync(sql, new { Id = id, Status = status });
        }

        public async Task<List<ServiceMeterReadings>> getbystatus(int status)
        {
            var sql = "SELECT * FROM servicemeterreadings WHERE status = @Status";
            var result = await connection.QueryAsync<ServiceMeterReadings>(sql, new { Status = status });
            return result.ToList();
        }
    }
}
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
    public class ServiceRepository(IDbConnection connection) : SimpleCrudRepository<Service, Guid>(connection)
    {
        public async Task<List<Service>> GetAllService()
        {
            var sql = SqlCommandHelper.GetSelectSql<Service>();
            var result = await connection.QueryAsync<Service>(sql);
            return result.ToList();
        }
        public async Task<Service?> GetByIDService(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Service>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Service> UpdateServiceAsyncRP(Service Service)
        {
            return await UpdateAsync(Service);
        }
        public async Task DeleteServiceAsyncRP(Service Service)
        {
            await DeleteAsync(Service);
        }
        public async Task<List<Service>> GetServicesByRoomId(Guid roomId)
        {
            var sql = @"
            SELECT s.*
            FROM service s
            INNER JOIN roomservice rs ON rs.""serviceid"" = s.""Id""
            WHERE rs.RoomId = @RoomId";

            var result = await connection.QueryAsync<Service>(sql, new { RoomId = roomId });
            return result.ToList();
        }
    }
}

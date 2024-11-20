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
    public class BookingRepository(IDbConnection connection) : SimpleCrudRepository<Bookings, Guid>(connection)
    {
        public async Task<List<Bookings>> GetAll()
        {
            var sql = SqlCommandHelper.GetSelectSql<Bookings>();
            var result = await connection.QueryAsync<Bookings>(sql);
            return result.ToList();
        }
        public async Task<Bookings?> GetByID(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Bookings>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Bookings> UpdateBookings(Bookings Bookings)
        {
            return await UpdateAsync(Bookings);
        }
        public async Task Delete(Bookings Bookings)
        {
            await DeleteAsync(Bookings);
        }
        public async Task<List<Bookings>> GetBKByBuildingId(Guid buildingId)
        {
            var sql = @"
            SELECT b.*
            FROM bookings b
            INNER JOIN room r ON b.""roomid"" = r.""Id""
            WHERE r.""Building_Id"" = @BuildingId";

            var result = await connection.QueryAsync<Bookings>(sql, new { BuildingId = buildingId });
            return result.ToList();
        }

    }
}
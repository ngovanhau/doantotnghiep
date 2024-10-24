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
    public class RoomRepository(IDbConnection connection) : SimpleCrudRepository<Room, Guid>(connection)
    {
        public async Task<List<Room>> GetAllRoom()
        {
            var sql = SqlCommandHelper.GetSelectSql<Room>();
            var result = await connection.QueryAsync<Room>(sql);
            return result.ToList();
        }
        public async Task<Room?> GetByIDRoom(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Room>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<List<Room>> GetAllRoomByBuildingID(Guid buildingId)
        {
            var param = new { Building_Id = buildingId };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Room>(new { Building_Id = buildingId });
            var result = await connection.QueryAsync<Room>(sql, param);
            return result.ToList();
        }
        public async Task<List<Room>> GetRoomByStatus(int status)
        {
            var param = new { status = status };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Room>(new { status = status });
            var result = await connection.QueryAsync<Room>(sql, param);
            return result.ToList();
        }
        public async Task<Room> UpdateRoomAsync(Room Room)
        {
            return await UpdateAsync(Room);
        }
        public async Task DeleteRoomAsync(Room Room)
        {
            await DeleteAsync(Room);
        }

        public async Task AddServicesToRoom(Guid roomId, List<Guid> serviceIds)
        {
            foreach (var serviceId in serviceIds)
            {
                var entry = new { RoomId = roomId, ServiceId = serviceId };
                var sql = "INSERT INTO roomservice (RoomId, ServiceId) VALUES (@RoomId, @ServiceId)";
                await connection.ExecuteAsync(sql, entry);
            }
        }
        public async Task RemoveServicesFromRoom(Guid roomId)
        {
            var sql = "DELETE FROM roomservice WHERE RoomId = @RoomId";
            await connection.ExecuteAsync(sql, new { RoomId = roomId });
        }
        public async Task<List<RoomserviceDto>> GetServicesByRoom(Guid roomId)
        {
            var sql = @"SELECT s.""Id"" as ServiceId, s.""service_name"" as ServiceName
                FROM roomservice bfs
                INNER JOIN service s ON bfs.""serviceid"" = s.""Id""
                WHERE bfs.""roomid"" = @RoomId";
            var result = await connection.QueryAsync<RoomserviceDto>(sql, new { RoomId = roomId });
            return result.ToList();
        }
        public async Task AddImagesToRoom(Guid roomId, List<string> imageUrls)
        {
            foreach (var imageUrl in imageUrls)
            {
                var entry = new { RoomId = roomId, ImageUrl = imageUrl };
                var sql = "INSERT INTO roomimage (RoomId, imageUrl) VALUES (@RoomId, @ImageUrl)";
                await connection.ExecuteAsync(sql, entry);
            }
        }

        public async Task RemoveImagesFromRoom(Guid roomId)
        {
            var sql = "DELETE FROM roomimage WHERE RoomId = @RoomId";
            await connection.ExecuteAsync(sql, new { RoomId = roomId });
        }

        public async Task<List<string>> GetImagesByRoom(Guid roomId)
        {
            var sql = "SELECT ImageUrl FROM roomimage WHERE RoomId = @RoomId";
            var result = await connection.QueryAsync<string>(sql, new { RoomId = roomId });
            return result.ToList();
        }
    }
}

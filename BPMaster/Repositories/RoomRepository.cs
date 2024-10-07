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
        public async Task<Room> UpdateRoomAsync(Room Room)
        {
            return await UpdateAsync(Room);
        }
        public async Task DeleteRoomAsync(Room Room)
        {
            await DeleteAsync(Room);
        }
    }
}

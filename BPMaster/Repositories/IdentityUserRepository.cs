using System.Data;
using BPMaster.Domains.Entities;
using Common.Databases;
using Common.Repositories;
using Dapper;
using Domain.Entities;

namespace Repositories
{
    public class IdentityUserRepository(IDbConnection connection) : SimpleCrudRepository<IdentityUser, Guid>(connection)
    {
        public async Task<IdentityUser?> GetByUsernameAsync(string username)
        {
            var param = new { Username = username };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<IdentityUser>(new { Username = username });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<IdentityUser?> GetByEmailAsync(string email)
        {
            var param = new { Email = email };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<IdentityUser>(new { Email = email });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<List<IdentityUser>> GetAllUser()
        {
            var sql = SqlCommandHelper.GetSelectSql<IdentityUser>();
            var result = await connection.QueryAsync<IdentityUser>(sql);
            return result.ToList();
        }
        public async Task<IdentityUser?> GetByIDUser(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<IdentityUser>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task DeleteUser(IdentityUser user)
        {
            await DeleteAsync(user);
        }
        public async Task Updateavata(Guid userId, string avata)
        {
            var sql = "UPDATE identity_users SET avata = @Avata WHERE \"Id\"= @UserId";
            await connection.ExecuteAsync(sql, new { UserId = userId, Avata = avata });
        }
    }
}

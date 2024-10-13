using System.Data;
using Common.Databases;
using Common.Repositories;
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
    }
}

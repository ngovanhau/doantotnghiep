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
    public class ProblemRepository(IDbConnection connection) : SimpleCrudRepository<Problem, Guid>(connection)
    {
        public async Task<List<Problem>> GetAllProblem()
        {
            var sql = SqlCommandHelper.GetSelectSql<Problem>();
            var result = await connection.QueryAsync<Problem>(sql);
            return result.ToList();
        }
        public async Task<Problem?> GetByIDProblem(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Problem>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Problem> UpdateProblem(Problem Problem)
        {
            return await UpdateAsync(Problem);
        }
        public async Task DeleteProblem(Problem Problem)
        {
            await DeleteAsync(Problem);
        }
    }
}


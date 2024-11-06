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
        public async Task AddImagesForProblem(Guid Id, List<string> imageUrls)
        {
            foreach (var imageUrl in imageUrls)
            {
                var entry = new { ProblemId = Id, ImageUrl = imageUrl };
                var sql = "INSERT INTO problemimage (ProblemId, ImageUrl) VALUES (@problemid, @imageurl)";
                await connection.ExecuteAsync(sql, entry);
            }
        }
        public async Task RemoveImagesFromProblem(Guid problemId)
        {
            var sql = "DELETE FROM problemimage WHERE problemid = @ProblemId";
            await connection.ExecuteAsync(sql, new { ProblemId = problemId });
        }

        public async Task<List<string>> GetImagesByProblem(Guid problemId)
        {
            var sql = "SELECT ImageUrl FROM problemimage WHERE problemid = @ProblemId";
            var result = await connection.QueryAsync<string>(sql, new { ProblemId = problemId });
            return result.ToList();
        }

        public async Task<List<Problem>> GetByRoomId(Guid id)
        {
            var sql = "SELECT * FROM problem WHERE roomid = @RoomId";
            var result = await connection.QueryAsync <Problem>(sql, new { RoomId = id });
            return result.ToList();
        }
    }
}


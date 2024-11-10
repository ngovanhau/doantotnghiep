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
    public class DepositRepository(IDbConnection connection) : SimpleCrudRepository<Deposit, Guid>(connection)
    {
        public async Task<List<Deposit>> GetAllDeposit()
        {
            var sql = SqlCommandHelper.GetSelectSql<Deposit>();
            var result = await connection.QueryAsync<Deposit>(sql);
            return result.ToList();
        }
        public async Task<Deposit?> GetByIDDeposit(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Deposit>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Deposit> UpdateDeposit(Deposit Deposit)
        {
            return await UpdateAsync(Deposit);
        }
        public async Task DeleteDeposit(Deposit Deposit)
        {
            await DeleteAsync(Deposit);
        }
        public async Task AddImagesToDeposit(Guid Id, List<string> imageUrls)
        {
            foreach (var imageUrl in imageUrls)
            {
                var entry = new { depositid = Id, ImageUrl = imageUrl };
                var sql = "INSERT INTO depositimage (depositid, imageUrl) VALUES (@depositid, @imageurl)";
                await connection.ExecuteAsync(sql, entry);
            }
        }
        public async Task RemoveImagesFromDeposit(Guid Id)
        {
            var sql = "DELETE FROM depositimage WHERE depositid = @DepositId";
            await connection.ExecuteAsync(sql, new { DepositId = Id });
        }

        public async Task<List<string>> GetImagesByDeposit(Guid Id)
        {
            var sql = "SELECT ImageUrl FROM depositimage WHERE depositid = @DepositId";
            var result = await connection.QueryAsync<string>(sql, new { DepositId = Id });
            return result.ToList();
        }
        public async Task UpdateStatusForDeposit(Guid depositId, int status)
        {
            var sql = "UPDATE deposit SET status = @Status WHERE \"Id\"= @DepositId";
            await connection.ExecuteAsync(sql, new { DepositId = depositId, Status = status });
        }
    }
}
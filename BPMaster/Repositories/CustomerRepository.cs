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
    public class CustomerRepository(IDbConnection connection) : SimpleCrudRepository<Customer, Guid>(connection)
    {
        public async Task<List<Customer>> GetAllCustomer()
        {
            var sql = SqlCommandHelper.GetSelectSql<Customer>();
            var result = await connection.QueryAsync<Customer>(sql);
            return result.ToList();
        }
        public async Task<Customer?> GetByIDCustomer(Guid id)
        {
            var param = new { Id = id };
            var sql = SqlCommandHelper.GetSelectSqlWithCondition<Customer>(new { Id = id });
            return await GetOneByConditionAsync(sql, param);
        }
        public async Task<Customer> UpdateCustomerAsyncRP(Customer customer)
        {
            return await UpdateAsync(customer);
        }
        public async Task DeleteCustomerAsyncRP(Customer customer)
        {
            await DeleteAsync(customer);
        }
        public async Task AddImagesToCustomer(Guid Id, List<string> imageUrls)
        {
            foreach (var imageUrl in imageUrls)
            {
                var entry = new { CustomerId = Id, ImageUrl = imageUrl };
                var sql = "INSERT INTO customerimage (CustomerId, imageUrl) VALUES (@customerid, @imageurl)";
                await connection.ExecuteAsync(sql, entry);
            }
        }
        public async Task RemoveImagesFromCustomer(Guid customerId)
        {
            var sql = "DELETE FROM customerimage WHERE customerId = @customerId";
            await connection.ExecuteAsync(sql, new { CustomerId = customerId });
        }

        public async Task<List<string>> GetImagesByCustomer(Guid customerId)
        {
            var sql = "SELECT ImageUrl FROM customerimage WHERE CustomerId = @CustomerId";
            var result = await connection.QueryAsync<string>(sql, new { CustomerId = customerId });
            return result.ToList();
        }
        public async Task UpdateRoomStatus(Guid? roomId, int status)
        {
            var sql = "UPDATE room SET status = @Status WHERE \"Id\" = @RoomId";
            await connection.ExecuteAsync(sql, new { RoomId = roomId, Status = status });
        }
        public async Task<Room> GetChooseRoombyId(Guid id)
        {
            var sql = "SELECT * FROM room WHERE \"Id\" = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Room>(sql, new { Id = id });
            return result;
        }
        public async Task UpdateChooseRoomForCustomer(Guid customerId, Guid chooseroom)
        {
            var sql = "UPDATE customer SET \"choose_room\" = @Chooseroom WHERE \"Id\" = @CustomerId";
            await connection.ExecuteAsync(sql, new { Chooseroom = chooseroom, CustomerId = customerId });
        }
        public async Task RemoveChooseRoomFromCustomer(Guid customerId, Guid chooseroom)
        {
            var sql = "UPDATE customer SET \"choose_room\" = @Chooseroom WHERE \"Id\" = @CustomerId";
            await connection.ExecuteAsync(sql, new { CustomerId = customerId, Chooseroom = chooseroom });
        }

    }
}

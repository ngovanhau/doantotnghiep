using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;

namespace BPMaster.Services
{
    [ScopedService]
    public class CustomerService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly CustomerRepository _CustomerRepository = new(connection);

        public async Task<List<Customer>> GetAllCustomer()
        {
            return await _CustomerRepository.GetAllCustomer();
        }

        public async Task<Customer> GetByIDCustomer(Guid CustomerId)
        {
            var Customer = await _CustomerRepository.GetByIDCustomer(CustomerId);

            if (Customer == null)
            {
                throw new NonAuthenticateException();
            }
            return Customer;
        }

        public async Task<Customer> CreateCustomerAsync(CustomerDto dto)
        {
            var Customer = _mapper.Map<Customer>(dto);

            Customer.Id = Guid.NewGuid();

            await _CustomerRepository.CreateAsync(Customer);

            return Customer;
        }
        public async Task<Customer> UpdateCustomerAsync(Guid id, CustomerDto dto)
        {
            var existingCustomer = await _CustomerRepository.GetByIDCustomer(id);

            if (existingCustomer == null)
            {
                throw new Exception("Error");
            }
            var Customer = _mapper.Map(dto, existingCustomer);

            await _CustomerRepository.UpdateAsync(Customer);

            return Customer;
        }
        public async Task DeleteCustomerAsync(Guid id)
        {
            var Customer = await _CustomerRepository.GetByIDCustomer(id);
            if (Customer == null)
            {
                throw new Exception("Customer not found !");
            }
            await _CustomerRepository.DeleteAsync(Customer);
        }

    }
}


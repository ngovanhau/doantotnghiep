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
    public class BillService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly BillRepository _Repository = new(connection);

        public async Task<List<BillDto>> GetAll()
        {
            var deposits = await _Repository.GetAll();
            var dto = _mapper.Map<List<BillDto>>(deposits);
            return dto;
        }

        public async Task<BillDto> GetById(Guid Id)
        {
            var bill = await _Repository.GetByID(Id);

            if (bill == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<BillDto>(bill);
            return dto;
        }
        public async Task<Bill> UpdateAsync(Guid id, BillDto dto)
        {
            var existing = await _Repository.GetByID(id);

            if (existing == null)
            {
                throw new Exception("not found !");
            }

            var bill = _mapper.Map(dto, existing);

            await _Repository.Updatebill(bill);

            return bill;
        }
        public async Task<Bill> CreateAsync(BillDto dto)
        {
            var Bill = _mapper.Map<Bill>(dto);

            Bill.Id = Guid.NewGuid();

            await _Repository.CreateAsync(Bill);

            return Bill;
        }
        public async Task DeleteAsync(Guid id)
        {
            var Bill = await _Repository.GetByID(id);
            if (Bill == null)
            {
                throw new Exception("Deposit not found !");
            }

            await _Repository.Delete(Bill);
        }
    }
}




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
    public class TransactionGroupService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly TransactionGroupRepository _Repository = new(connection);

        public async Task<List<TransactionGroupDto>> GetAll()
        {
            var transactiongroup = await _Repository.GetAll();
            var dto = _mapper.Map<List<TransactionGroupDto>>(transactiongroup);
            return dto;
        }

        public async Task<TransactionGroupDto> GetById(Guid Id)
        {
            var transactiongroup = await _Repository.GetByID(Id);

            if (transactiongroup == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<TransactionGroupDto>(transactiongroup);
            return dto;
        }
        public async Task<List<TransactionGroupDto>> GetBytype(int type)
        {
            var transactiongroup = await _Repository.GetByType(type);

            if (transactiongroup == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<List<TransactionGroupDto>>(transactiongroup);
            return dto;
        }
        public async Task<TransactionGroup> UpdateAsync(Guid id, TransactionGroupDto dto)
        {
            var existing = await _Repository.GetByID(id);

            if (existing == null)
            {
                throw new Exception("not found !");
            }

            var transactiongroup = _mapper.Map(dto, existing);

            await _Repository.UpdateTransactionGroup(transactiongroup);

            return transactiongroup;
        }
        public async Task<TransactionGroup> CreateAsync(TransactionGroupDto dto)
        {
            var transactiongroup = _mapper.Map<TransactionGroup>(dto);

            transactiongroup.Id = Guid.NewGuid();

            await _Repository.CreateAsync(transactiongroup);

            return transactiongroup;
        }
        public async Task DeleteAsync(Guid id)
        {
            var transactiongroup = await _Repository.GetByID(id);
            if (transactiongroup == null)
            {
                throw new Exception("Deposit not found !");
            }

            await _Repository.Delete(transactiongroup);
        }
    }
}




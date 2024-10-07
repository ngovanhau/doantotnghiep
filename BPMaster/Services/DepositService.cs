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
    public class DepositService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly DepositRepository _DepositRepository = new(connection);

        public async Task<List<Deposit>> GetAllDeposit()
        {
            return await _DepositRepository.GetAllDeposit();
        }

        public async Task<Deposit> GetByIDDeposit(Guid DepositId)
        {
            var Deposit = await _DepositRepository.GetByIDDeposit(DepositId);

            if (Deposit == null)
            {
                throw new NonAuthenticateException();
            }
            return Deposit;
        }

        public async Task<Deposit> CreateDepositAsync(DepositDto dto)
        {
            var Deposit = _mapper.Map<Deposit>(dto);

            Deposit.Id = Guid.NewGuid();

            await _DepositRepository.CreateAsync(Deposit);

            return Deposit;
        }
        public async Task<Deposit> UpdateDepositAsync(Guid id, DepositDto dto)
        {
            var existingDeposit = await _DepositRepository.GetByIDDeposit(id);

            if (existingDeposit == null)
            {
                throw new Exception("Deposit not found !");
            }
            var Deposit = _mapper.Map(dto, existingDeposit);

            await _DepositRepository.UpdateAsync(Deposit);

            return Deposit;
        }
        public async Task DeleteDepositAsync(Guid id)
        {
            var Deposit = await _DepositRepository.GetByIDDeposit(id);
            if (Deposit == null)
            {
                throw new Exception("Deposit not found !");
            }
            await _DepositRepository.DeleteAsync(Deposit);
        }

    }
}




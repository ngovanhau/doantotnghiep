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
    public class ContractService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly ContractRepository _ContractRepository = new(connection);

        public async Task<List<Contract>> GetAllContract()
        {
            return await _ContractRepository.GetAllContract();
        }

        public async Task<Contract> GetByIDContract(Guid ContractId)
        {
            var Contract = await _ContractRepository.GetByIDContract(ContractId);

            if (Contract == null)
            {
                throw new NonAuthenticateException();
            }
            return Contract;
        }

        public async Task<Contract> CreateContractAsync(ContractDto dto)
        {
            var Contract = _mapper.Map<Contract>(dto);

            Contract.Id = Guid.NewGuid();

            await _ContractRepository.CreateAsync(Contract);

            return Contract;
        }
        public async Task<Contract> UpdateContractAsync(Guid id, ContractDto dto)
        {
            var existingContract = await _ContractRepository.GetByIDContract(id);

            if (existingContract == null)
            {
                throw new Exception("Contract not found !");
            }
            var Contract = _mapper.Map(dto, existingContract);

            await _ContractRepository.UpdateAsync(Contract);

            return Contract;
        }
        public async Task DeleteContractAsync(Guid id)
        {
            var Contract = await _ContractRepository.GetByIDContract(id);
            if (Contract == null)
            {
                throw new Exception("Contract not found !");
            }
            await _ContractRepository.DeleteAsync(Contract);
        }

    }
}



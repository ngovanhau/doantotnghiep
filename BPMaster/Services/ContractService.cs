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
        private readonly CustomerRepository _CustomerRepository = new(connection);
        private readonly RoomRepository _RoomRepository = new(connection);

        public async Task<List<ContractDto>> GetAllContract()
        {
            var contracts = await _ContractRepository.GetAllContract();
            var result = new List<ContractDto>();

            foreach (var contract in contracts) {
                var dto = _mapper.Map<ContractDto>(contract);
                if (dto.CustomerId != Guid.Empty)
                {
                    var tenantname = await _CustomerRepository.GetByIDCustomer(dto.CustomerId);
                    if (tenantname != null)
                    {
                        dto.CustomerName = tenantname.customer_name;
                    }
                }
                result.Add(dto);
            }
            return result;
        }

        public async Task<ContractDto> GetByIDContract(Guid ContractId)
        {
            var Contract = await _ContractRepository.GetByIDContract(ContractId);

            if (Contract == null)
            {
                throw new NonAuthenticateException();
            }

            var dto = _mapper.Map<ContractDto>(Contract);

            if (Contract.CustomerId != Guid.Empty) 
            {
                var tenantname = await _CustomerRepository.GetByIDCustomer(Contract.CustomerId);

                if (tenantname != null) {
                    dto.CustomerName = tenantname.customer_name;
                }
                
            }
            return dto;
        }

        public async Task<Contract> CreateContractAsync(ContractDto dto)
        {
            var Contract = _mapper.Map<Contract>(dto);  

            Contract.Id = Guid.NewGuid();

            await _RoomRepository.UpdateStatusForRoom(Contract.roomId,1);

            await _RoomRepository.UpdateCustomerIDforRoom(Contract.roomId, Contract.CustomerId);

            await _CustomerRepository.UpdateChooseRoomForCustomer(Contract.CustomerId, Contract.roomId);

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

            if(existingContract.roomId != dto.roomId)
            {

                await _RoomRepository.UpdateStatusForRoom(existingContract.roomId, 0);

                Guid IdRoomnoexist = Guid.NewGuid();

                await _RoomRepository.UpdateCustomerIDforRoom(existingContract.roomId, IdRoomnoexist);

                await _RoomRepository.UpdateCustomerIDforRoom(dto.roomId, dto.CustomerId);
            }

            if(existingContract.CustomerId != dto.CustomerId)
            {
                Guid Idnoexist = Guid.NewGuid();

                await _CustomerRepository.RemoveChooseRoomFromCustomer(existingContract.CustomerId, Idnoexist);

                await _CustomerRepository.UpdateChooseRoomForCustomer(dto.CustomerId, dto.roomId);
                
            }

            var Contract = _mapper.Map<Contract>(dto);

            await _RoomRepository.UpdateStatusForRoom(Contract.roomId, 1);

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

             Guid IdRoomnoexist = Guid.NewGuid();

            await _RoomRepository.UpdateCustomerIDforRoom(Contract.roomId, IdRoomnoexist);

            Guid Idnoexist = Guid.NewGuid();

            await _CustomerRepository.RemoveChooseRoomFromCustomer(Contract.CustomerId, Idnoexist);

            await _ContractRepository.DeleteAsync(Contract);
        }

    }
}



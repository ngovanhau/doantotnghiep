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
        private readonly RoomRepository _RoomRepo = new(connection);

        public async Task<List<DepositDto>> GetAllDeposit()
        {
            var deposits = await _DepositRepository.GetAllDeposit();
            var result = new List<DepositDto>();

            foreach (var deposit in deposits) 
            {
                var image = await _DepositRepository.GetImagesByDeposit(deposit.Id);

                var dto = _mapper.Map<DepositDto>(deposit);

                dto.image = image;

                result.Add(dto);
            }
            return result;
        }

        public async Task<DepositDto> GetByIDDeposit(Guid DepositId)
        {
            var Deposit = await _DepositRepository.GetByIDDeposit(DepositId);

            if (Deposit == null)
            {
                throw new NonAuthenticateException();
            }

            var image = await _DepositRepository.GetImagesByDeposit(DepositId);

            var dto = _mapper.Map<DepositDto>(Deposit);

            dto.image = image;

            return dto;
        }

        public async Task<Deposit> CreateDepositAsync(DepositDto dto)
        {
            var Deposit = _mapper.Map<Deposit>(dto);

            await _RoomRepo.UpdateStatusForRoom(dto.roomid, 3);

            Deposit.Id = Guid.NewGuid();

            await _DepositRepository.CreateAsync(Deposit);

            await _DepositRepository.AddImagesToDeposit(Deposit.Id, dto.image);

            return Deposit;
        }
        public async Task<Deposit> UpdateDepositAsync(Guid id, DepositDto dto)
        {
            var existingDeposit = await _DepositRepository.GetByIDDeposit(id);

            if (existingDeposit == null)
            {
                throw new Exception("Deposit not found !");
            }

            await _DepositRepository.RemoveImagesFromDeposit(id);

            if (dto.image != null) 
            {
                await _DepositRepository.AddImagesToDeposit(id, dto.image);
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

            await _DepositRepository.RemoveImagesFromDeposit(id);

            await _DepositRepository.DeleteAsync(Deposit);
        }
        public async Task updateStatus(Guid id, int status)
        {
            var deposit= await _DepositRepository.GetByIDDeposit(id);

            if (deposit == null)
            {
                throw new Exception("Deposit not found !");
            }

            await _DepositRepository.UpdateStatusForDeposit(id, status);
        }
    }
}




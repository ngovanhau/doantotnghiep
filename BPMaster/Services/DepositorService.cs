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
    public class DepositorService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly DepositorRepository _DepositorRepository = new(connection);

        public async Task<List<Depositor>> GetAllDepositor()
        {
            return await _DepositorRepository.GetAllDepositor();
        }

        public async Task<Depositor> GetByIDDepositor(Guid DepositorId)
        {
            var Depositor = await _DepositorRepository.GetByIDDepositor(DepositorId);

            if (Depositor == null)
            {
                throw new NonAuthenticateException();
            }
            return Depositor;
        }

        public async Task<Depositor> CreateDepositorAsync(DepositorDto dto)
        {
            var Depositor = _mapper.Map<Depositor>(dto);

            Depositor.Id = Guid.NewGuid();

            await _DepositorRepository.CreateAsync(Depositor);

            return Depositor;
        }
        public async Task<Depositor> UpdateDepositorAsync(Guid id, DepositorDto dto)
        {
            var existingDepositor = await _DepositorRepository.GetByIDDepositor(id);

            if (existingDepositor == null)
            {
                throw new Exception("Depositor not found !");
            }
            var Depositor = _mapper.Map(dto, existingDepositor);

            await _DepositorRepository.UpdateAsync(Depositor);

            return Depositor;
        }
        public async Task DeleteDepositorAsync(Guid id)
        {
            var Depositor = await _DepositorRepository.GetByIDDepositor(id);
            if (Depositor == null)
            {
                throw new Exception("Depositor not found !");
            }
            await _DepositorRepository.DeleteAsync(Depositor);
        }

    }
}




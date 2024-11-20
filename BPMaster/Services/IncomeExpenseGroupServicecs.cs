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
    public class IncomeExpenseGroupService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly IncomeExpenseGroupRepository _Repository = new(connection);

        public async Task<List<IncomeExpenseGroupDto>> GetAll()
        {
            var incomeexpensegroup = await _Repository.GetAll(); 
            var dto = _mapper.Map<List<IncomeExpenseGroupDto>>(incomeexpensegroup);
            return dto;
        }

        public async Task<IncomeExpenseGroupDto> GetById(Guid Id)
        {
            var incomeexpensegroup = await _Repository.GetByID(Id);

            if (incomeexpensegroup == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<IncomeExpenseGroupDto>(incomeexpensegroup);
            return dto;
        }

        public async Task<List<IncomeExpenseGroupDto>> GetByBuildingId(Guid Id)
        {
            var incomeexpensegroup = await _Repository.getByBuildingId(Id);

            if (incomeexpensegroup == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<List<IncomeExpenseGroupDto>>(incomeexpensegroup);

            return dto;
        }
        public async Task<List<IncomeExpenseGroupDto>> GetByRoomId(Guid Id)
        {
            var incomeexpensegroup = await _Repository.getByRoomId(Id);

            if (incomeexpensegroup == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<List<IncomeExpenseGroupDto>>(incomeexpensegroup);

            return dto;
        }
        public async Task<IncomeExpenseGroup> UpdateAsync(Guid id, IncomeExpenseGroupDto dto)
        {
            var existing = await _Repository.GetByID(id);

            if (existing == null)
            {
                throw new Exception("not found !");
            }

            var incomeexpensegroup = _mapper.Map(dto, existing);

            await _Repository.Update(incomeexpensegroup);

            return incomeexpensegroup;
        }
        public async Task<IncomeExpenseGroup> CreateAsync(IncomeExpenseGroupDto dto)
        {
            var incomeexpensegroup = _mapper.Map<IncomeExpenseGroup>(dto);

            incomeexpensegroup.Id = Guid.NewGuid();

            await _Repository.CreateAsync(incomeexpensegroup);

            return incomeexpensegroup;
        }
        public async Task DeleteAsync(Guid id)
        {
            var incomeexpensegroup = await _Repository.GetByID(id);
            if (incomeexpensegroup == null)
            {
                throw new Exception("Deposit not found !");
            }

            await _Repository.Delete(incomeexpensegroup);
        }
    }
}




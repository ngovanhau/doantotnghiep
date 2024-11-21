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
    public class DashboardService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly DashboardRepository _Repository = new(connection);

        public async Task<DashboardDto> GetAll()
        {
            var deposits = await _Repository.GetDashboardSummary();
            var dto = _mapper.Map<DashboardDto>(deposits);
            return dto;
        }

    }
}




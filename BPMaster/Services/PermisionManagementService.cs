using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;
using System.Security;

namespace BPMaster.Services
{
    [ScopedService]
    public class PermisionManagementService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly PermisionManagementRepository _Repository = new(connection);

        public async Task<List<PermissionManagement>> GetAll()
        {
            return await _Repository.GetAll();
        }

        public async Task<PermissionManagement> GetByID(Guid Id)
        {
            var Permission = await _Repository.GetByID(Id);

            if (Permission == null)
            {
                throw new NonAuthenticateException();
            }
            return Permission;
        }

        public async Task<List<PermissionManagement>> GetUserByBuildingID(Guid Id)
        {
            var Permission = await _Repository.GetUserByBuildingId(Id);

            if (Permission == null)
            {
                throw new NonAuthenticateException();
            }
            return Permission;
        }

        public async Task<PermissionManagement> CreateAsync(PermissionManagementDto dto)
        {
            var Permission = _mapper.Map<PermissionManagement>(dto);

            Permission.Id = Guid.NewGuid();

            await _Repository.CreateAsync(Permission);

            return Permission;
        }
        public async Task<PermissionManagement> UpdateAsync(Guid id, PermissionManagementDto dto)
        {
            var existing = await _Repository.GetByID(id);

            if (existing == null)
            {
                throw new Exception("Depositor not found !");
            }
            var PermissionManagement = _mapper.Map(dto, existing);

            await _Repository.UpdateAsync(PermissionManagement);

            return PermissionManagement;
        }
        public async Task DeleteAsync(Guid id)
        {
            var PermissionManagement = await _Repository.GetByID(id);
            if (PermissionManagement == null)
            {
                throw new Exception("Depositor not found !");
            }
            await _Repository.DeleteAsync(PermissionManagement);
        }
    }
}




using Application.Settings;
using BPMaster.Domains.Dtos;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;
using BPMaster.Domains.Entities;

namespace BPMaster.Controllers.v1
{
    public class PermisionController(IServiceProvider service) : BaseV1Controller<PermisionManagementService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all PermissionManagement
        /// </summary>
        [HttpGet("getallPermisionManagement")]
        public async Task<IActionResult> Getall()
        {
            var Deposit = await _service.GetAll();
            return Success(Deposit);
        }
        /// <summary>
        /// this is api get by id PermissionManagement
        /// </summary>
        [HttpGet("getuserbybuildingid")]
        public async Task<IActionResult> GetUserByBuildingId(Guid id)
        {
            return Success(await _service.GetUserByBuildingID(id));
        }
        /// <summary>
        /// this is api get by id PermissionManagement
        /// </summary>
        [HttpGet("getPermisionManagementbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Success(await _service.GetByID(id));
        }
        /// <summary>
        /// this is api create a new PermissionManagement
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PermissionManagementDto dto)
        {
            return CreatedSuccess(await _service.CreateAsync(dto));
        }
        /// <summary>
        /// this is api update PermissionManagement
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PermissionManagementDto dto)
        {
            return Success(await _service.UpdateAsync(id, dto));
        }
        /// <summary>
        /// this is api delete PermissionManagement
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Success("delete Success");
        }
    }
}


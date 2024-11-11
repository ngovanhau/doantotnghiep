using Application.Settings;
using BPMaster.Domains.Dtos;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;

namespace BPMaster.Controllers.v1
{
    public class DepositController(IServiceProvider service) : BaseV1Controller<DepositService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Deposit
        /// </summary>
        [HttpGet("Depositall")]
        public async Task<IActionResult> GetallDeposit()
        {
            var Deposit = await _service.GetAllDeposit();
            return Success(Deposit);
        }
        /// <summary>
        /// this is api get all Depositor by building id
        /// </summary>
        [HttpGet("Depositorallbybuildingid")]
        public async Task<IActionResult> GetallDepositorByBuildingId(Guid id)
        {
            var Depositor = await _service.GetAllByBuildingId(id);
            return Success(Depositor);
        }
        /// <summary>
        /// this is api get by id Deposit
        /// </summary>
        [HttpGet("getDepositbyid")]
        public async Task<IActionResult> GetDepositById(Guid id)
        {
            return Success(await _service.GetByIDDeposit(id));
        }
        /// <summary>
        /// this is api create a new Deposit
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateDeposit([FromBody] DepositDto dto)
        {
            return CreatedSuccess(await _service.CreateDepositAsync(dto));
        }
        /// <summary>
        /// this is api update status Deposit
        /// </summary>
        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateDeposit(Guid id, int status)
        {
            await _service.updateStatus(id, status);
            return Success("update status Success");
        }
        /// <summary>
        /// this is api update Deposit
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDeposit(Guid id, [FromBody] DepositDto dto)
        {
            return Success(await _service.UpdateDepositAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Deposit
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDeposit(Guid id)
        {
            await _service.DeleteDepositAsync(id);
            return Success("delete Success");
        }
    }
}


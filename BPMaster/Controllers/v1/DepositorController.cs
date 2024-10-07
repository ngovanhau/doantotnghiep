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
    public class DepositorController(IServiceProvider service) : BaseV1Controller<DepositorService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Depositor
        /// </summary>
        [HttpGet("Depositorall")]
        public async Task<IActionResult> GetallDepositor()
        {
            var Depositor = await _service.GetAllDepositor();
            return Success(Depositor);
        }
        /// <summary>
        /// this is api get by id Depositor
        /// </summary>
        [HttpGet("getDepositorbyid")]
        public async Task<IActionResult> GetDepositorById(Guid id)
        {
            return Success(await _service.GetByIDDepositor(id));
        }
        /// <summary>
        /// this is api create a new Depositor
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateDepositor([FromBody] DepositorDto dto)
        {
            return CreatedSuccess(await _service.CreateDepositorAsync(dto));
        }
        /// <summary>
        /// this is api update Depositor
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDepositor(Guid id, [FromBody] DepositorDto dto)
        {
            return Success(await _service.UpdateDepositorAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Depositor
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDepositor(Guid id)
        {
            await _service.DeleteDepositorAsync(id);
            return Success("delete Success");
        }
    }
}



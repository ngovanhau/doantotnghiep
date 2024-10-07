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
    public class ServiceController(IServiceProvider service) : BaseV1Controller<ServiceService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Service
        /// </summary>
        [HttpGet("Serviceall")]
        public async Task<IActionResult> GetService()
        {
            var Service = await _service.GetAllService();
            return Success(Service);
        }
        /// <summary>
        /// this is api get by id Service
        /// </summary>
        [HttpGet("getServicebyid")]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            return Success(await _service.GetByIDService(id));
        }
        /// <summary>
        /// this is api create a new Service
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateService([FromBody] ServiceDto dto)
        {
            return CreatedSuccess(await _service.CreateServiceAsync(dto));
        }
        /// <summary>
        /// this is api update Service
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceDto dto)
        {
            return Success(await _service.UpdateServiceAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Service
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            await _service.DeleteServiceAsync(id);
            return Success("delete Success");
        }
    }
}



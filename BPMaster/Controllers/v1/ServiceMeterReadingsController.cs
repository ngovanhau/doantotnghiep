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
    public class ServiceMeterReadingsController(IServiceProvider service) : BaseV1Controller<ServiceMeterReadingsSevice, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all ServiceMeterReadingsController
        /// </summary>
        [HttpGet("Serviceall")]
        public async Task<IActionResult> GetService()
        {
            var Service = await _service.GetAll();
            return Success(Service);
        }
        /// <summary>
        /// this is api get by id ServiceMeterReadings
        /// </summary>
        [HttpGet("getServicebyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Success(await _service.GetByID(id));
        }
        /// <summary>
        /// this is api get by roomid ServiceMeterReadings 
        /// </summary>
        [HttpGet("getServicebyroomid")]
        public async Task<IActionResult> GetByroomId(Guid id)
        {
            return Success(await _service.getbyroomid(id));
        }
        /// <summary>
        /// this is api create a new ServiceMeterReadings
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ServiceMeterReadingsDto dto)
        {
            return CreatedSuccess(await _service.CreateAsync(dto));
        }
        /// <summary>
        /// this is api update ServiceMeterReadings
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceMeterReadingsDto dto)
        {
            return Success(await _service.UpdateAsync(id, dto));
        }
        /// <summary>
        /// this is api delete ServiceMeterReadings
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Success("delete Success");
        }
    }
}



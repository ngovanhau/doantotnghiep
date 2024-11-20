using Application.Settings;
using BPMaster.Domains.Dtos;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace BPMaster.Controllers.v1
{
    public class BookingController(IServiceProvider service) : BaseV1Controller<BookingsService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Booking
        /// </summary>
        [HttpGet("Bookingall")]
        public async Task<IActionResult> GetAll()
        {
            var Building = await _service.GetAll();
            return Success(Building);
        }
        /// <summary>
        /// this is api get by id Booking
        /// </summary>
        [HttpGet("getBookingbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Success(await _service.GetById(id));
        }
        /// <summary>
        /// this is api get by buildingid Booking
        /// </summary>
        [HttpGet("getBookingbybuildingid")]
        public async Task<IActionResult> GetByBuildingId(Guid id)
        {
            return Success(await _service.GetByBuildingId(id));
        }
        /// <summary>
        /// this is api create a new Booking
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BookingsDto dto)
        {
            return CreatedSuccess(await _service.CreateAsync(dto));
        }
        /// <summary>
        /// this is api update Booking
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookingsDto dto)
        {
            return Success(await _service.UpdateAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Booking
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Success("delete Success");
        }
    }
}

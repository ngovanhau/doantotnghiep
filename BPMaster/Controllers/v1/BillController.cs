﻿using Application.Settings;
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
    public class BillController(IServiceProvider service) : BaseV1Controller<BillService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Bill
        /// </summary>
        [HttpGet("Billall")]
        public async Task<IActionResult> GetAll()
        {
            var Building = await _service.GetAll();
            return Success(Building);
        }
        /// <summary>
        /// this is api get by id Bill
        /// </summary>
        [HttpGet("getbillbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Success(await _service.GetById(id));
        }
        /// <summary>
        /// this is api get by id Bill
        /// </summary>
        [HttpGet("getbillbybuildingid")]
        public async Task<IActionResult> GetByBuildingId(Guid id)
        {
            return Success(await _service.GetByBuildingId(id));
        }
        /// <summary>
        /// this is api get by id Bill
        /// </summary>
        [HttpGet("getbillbyroomid")]
        public async Task<IActionResult> GetByroomId(Guid id)
        {
            return Success(await _service.GetByRoomId(id)); 
        }
        /// <summary>
        /// this is api create a new Bill
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BillDto dto)
        {
            return CreatedSuccess(await _service.CreateAsync(dto));
        }
        /// <summary>
        /// this is api update Bill
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BillDto dto)
        {
            return Success(await _service.UpdateAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Bill
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Success("delete Success");
        }
        /// <summary>
        /// this is api create payment
        /// </summary>
        [HttpPost("createpayment")]
        public async Task<IActionResult> CreatePayment(Guid billId)
        {
            var paymentUrl = await _service.CreateVNPayPayment(billId);
            return Success(paymentUrl);
        }
        /// <summary>
        /// this is api return payment
        /// </summary>
        [HttpPut("updatepaymentstatus")]
        public async Task<IActionResult> UpdateStatusPayment(Guid id)
        {
            return Success(await _service.Updatepaymentstatus(id));
        }
    }
}

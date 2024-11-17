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
    public class TransactionGroupController(IServiceProvider service) : BaseV1Controller<TransactionGroupService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all transactiongroup
        /// </summary>
        [HttpGet("getalltransactiongroup")]
        public async Task<IActionResult> GetAll()
        {
            var transactiongroup = await _service.GetAll();
            return Success(transactiongroup);
        }
        /// <summary>
        /// this is api get by id transactiongroup
        /// </summary>
        [HttpGet("getbyidtransactiongroup")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Success(await _service.GetById(id));
        }
        /// <summary>
        /// this is api get by id transactiongroup
        /// </summary>
        [HttpGet("getbytypetransactiongroup")]
        public async Task<IActionResult> GetBytransactiongroupId(int type)
        {
            return Success(await _service.GetBytype(type));
        }
        /// <summary>
        /// this is api create a new transactiongroup
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TransactionGroupDto dto)
        {
            return CreatedSuccess(await _service.CreateAsync(dto));
        }
        /// <summary>
        /// this is api update transactiongroup
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionGroupDto dto)
        {
            return Success(await _service.UpdateAsync(id, dto));
        }
        /// <summary>
        /// this is api delete transactiongroup
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Success("delete Success");
        }
    }
}

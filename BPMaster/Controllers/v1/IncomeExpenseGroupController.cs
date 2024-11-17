using Application.Settings;
using BPMaster.Domains.Dtos;
using BPMaster.Services;
using Common.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using Services;
using Microsoft.AspNetCore.Authorization;
using BPMaster.Domains.Entities;

namespace BPMaster.Controllers.v1
{
    public class IncomeExpenseGroupController(IServiceProvider service) : BaseV1Controller<IncomeExpenseGroupService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all incomeexpensegroupincomeexpensegroup
        /// </summary>
        [HttpGet("incomeexpensegroupall")]
        public async Task<IActionResult> GetAll()
        {
            var IncomeExpenseGroupDto = await _service.GetAll();
            return Success(IncomeExpenseGroupDto);
        }
        /// <summary>
        /// this is api get by id incomeexpensegroup
        /// </summary>
        [HttpGet("getincomeexpensegroupbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Success(await _service.GetById(id));
        }
        /// <summary>
        /// this is api create a new incomeexpensegroup
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] IncomeExpenseGroupDto dto)
        {
            return CreatedSuccess(await _service.CreateAsync(dto));
        }
        /// <summary>
        /// this is api update incomeexpensegroup
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] IncomeExpenseGroupDto dto)
        {
            return Success(await _service.UpdateAsync(id, dto));
        }
        /// <summary>
        /// this is api delete incomeexpensegroup
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Success("delete Success");
        }
    }
}

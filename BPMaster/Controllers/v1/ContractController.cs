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
    public class ContractController(IServiceProvider service) : BaseV1Controller<ContractService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Contract
        /// </summary>
        [HttpGet("Contractall")]
        public async Task<IActionResult> GetallContract()
        {
            var Contract = await _service.GetAllContract();
            return Success(Contract);
        }
        /// <summary>
        /// this is api get by building id Contract
        /// </summary>
        [HttpGet("getContractbybuildingid")]
        public async Task<IActionResult> GetContractBybuildingId(Guid id)
        {
            return Success(await _service.GetAllByBuildingId(id));
        }
        /// <summary>
        /// this is api get by id Contract
        /// </summary>
        [HttpGet("getContractbyid")]
        public async Task<IActionResult> GetContractById(Guid id)
        {
            return Success(await _service.GetByIDContract(id));
        }
        /// <summary>
        /// this is api create a new Contract
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateContract([FromBody] ContractDto dto)
        {
            return CreatedSuccess(await _service.CreateContractAsync(dto));
        }
        /// <summary>
        /// this is api update Contract
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateContract(Guid id, [FromBody] ContractDto dto)
        {
            return Success(await _service.UpdateContractAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Contract
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            await _service.DeleteContractAsync(id);
            return Success("delete Success");
        }
    }
}


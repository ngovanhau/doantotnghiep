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
    public class ProblemController(IServiceProvider service) : BaseV1Controller<ProblemService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Problem
        /// </summary>
        [HttpGet("Problemall")]
        public async Task<IActionResult> GetProblem()
        {
            var Problem = await _service.GetAllProblem();
            return Success(Problem);
        }
        /// <summary>
        /// this is api get by id Problem
        /// </summary>
        [HttpGet("getProblembyid")]
        public async Task<IActionResult> GetProblemById(Guid id)
        {
            return Success(await _service.GetByIDProblem(id));
        }
        /// <summary>
        /// this is api get problem by 
        /// </summary>
        [HttpGet("getProblembyroomid")]
        public async Task<IActionResult> GetProblemByRoomId(Guid id)
        {
            return Success(await _service.GetProblemByRoomId(id));
        }
        /// <summary>
        /// this is api get problem by 
        /// </summary>
        [HttpGet("getProblembybuildingid")]
        public async Task<IActionResult> GetProblemByBuildingId(Guid id)
        {
            return Success(await _service.getByBuildingId(id));
        }
        /// <summary>
        /// this is api create a new Problem
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateProblem([FromBody] ProblemDto dto)
        {
            return CreatedSuccess(await _service.CreateProblemAsync(dto));
        }
        /// <summary>
        /// this is api update Problem
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProblem(Guid id, [FromBody] ProblemDto dto)
        {
            return Success(await _service.UpdateProblemAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Problem
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Deleteproduct(Guid id)
        {
            await _service.DeleteProblemAsync(id);
            return Success("delete Success");
        }
    }
}


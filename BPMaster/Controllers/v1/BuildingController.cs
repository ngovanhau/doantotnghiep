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
    public class BuildingController(IServiceProvider service) : BaseV1Controller<BuildingService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all Building
        /// </summary>
        [HttpGet("Buildingall")]
        public async Task<IActionResult> GetBuilding()
        {
            var Building = await _service.GetAllBuildings();
            return Success(Building);
        }
        /// <summary>
        /// this is api get by id Building
        /// </summary>
        [HttpGet("getBuildingbyid")]
        public async Task<IActionResult> GetBuildingById(Guid id)
        {
            return Success(await _service.GetByIDBuilding(id));
        }
        /// <summary>
        /// this is api get by user 
        /// </summary>
        [HttpGet("getBuildingbyuserid")]
        public async Task<IActionResult> GetBuildingByUserId(Guid id)
        {
            return Success(await _service.GetBuildingByUserId(id));
        }
        /// <summary>
        /// this is api create a new Building
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateBuilding([FromBody] BuildingDto dto)
        {
            return CreatedSuccess(await _service.CreateBuildingAsync(dto));
        }
        /// <summary>
        /// this is api update Building
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBuilding(Guid id, [FromBody] BuildingDto dto)
        {
            return Success(await _service.UpdateBuildingAsync(id, dto));
        }
        /// <summary>
        /// this is api delete Building
        /// </summary>
        [HttpDelete("delete")]
        public async Task<IActionResult> Deleteproduct(Guid id)
        {
            await _service.DeleteBuildingAsync(id);
            return Success("delete Success");
        }
    }
}

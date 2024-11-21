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
    public class DashboardController(IServiceProvider service) : BaseV1Controller<DashboardService, ApplicationSetting>(service)
    {
        /// <summary>
        /// this is api get all dashboard
        /// </summary>
        [HttpGet("dashboardall")]
        public async Task<IActionResult> GetAll()
        {
            var Building = await _service.GetAll();
            return Success(Building);
        }
    }
}

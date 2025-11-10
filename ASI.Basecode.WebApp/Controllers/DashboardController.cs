using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        
        /// Get dashboard statistics
        
        [HttpGet("statistics")]
        [AllowAnonymous]
        public ActionResult<DashboardStatisticsViewModel> GetStatistics()
        {
            try
            {
                var statistics = _dashboardService.GetDashboardStatistics();
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}

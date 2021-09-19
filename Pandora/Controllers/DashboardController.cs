using Microsoft.AspNetCore.Mvc;
using Pandora.Application.Contract;
using Pandora.Application.Service;
using Pandora.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dashboards = _dashboardService.GetAll();
            return Ok(dashboards);
        }

    }
}

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
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var plans = await _planService.GetAll();
            return Ok(plans);
        }

    }
}

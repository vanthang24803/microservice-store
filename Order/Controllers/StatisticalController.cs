using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Interfaces;
using Order.Core.Utils;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/order/statistical")]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatistical _statistical;

        public StatisticalController(IStatistical statistical)
        {
            _statistical = statistical;
        }

        [HttpGet]

        public async Task<IActionResult> GetStatistical([FromQuery] QueryObject query)
        {
            var result = await _statistical.CreateAsync(query);

            return Ok(result);
        }
    }
}
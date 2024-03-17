using Microsoft.AspNetCore.Mvc;
using Product.Core.Interfaces;
using Product.Core.Utils;

namespace Product.Controllers
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

        public async Task<IActionResult> GetStatistical([FromQuery] QueryObjectOrder query)
        {
            var result = await _statistical.CreateAsync(query);

            return Ok(result);
        }
    }
}
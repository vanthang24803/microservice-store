
using Microsoft.AspNetCore.Mvc;
using Product.Context;
using Microsoft.EntityFrameworkCore;
using Product.Core.Enum;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/order/chart")]
    public class ChartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, double>>> GetMonthlyRevenue()
        {
            // Get all paid orders
            var orders = await _context.Orders
                .Where(o => o.Status == Status.SUCCESS)
                .ToListAsync();

            // Initialize revenue data
            var monthlyRevenue = new Dictionary<string, double>
            {
                {"Jan", 0}, {"Feb", 0}, {"Mar", 0}, {"Apr", 0},
                {"May", 0}, {"Jun", 0}, {"Jul", 0}, {"Aug", 0},
                {"Sep", 0}, {"Oct", 0}, {"Nov", 0}, {"Dec", 0}
            };

            // Calculate monthly revenue
            foreach (var order in orders)
            {
                var month = order.CreateAt.ToString("MMM");
                monthlyRevenue[month] += order.TotalPrice;
            }

            return Ok(monthlyRevenue);
        }
    }
}
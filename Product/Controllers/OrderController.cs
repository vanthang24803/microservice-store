using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Order;
using Product.Core.Interfaces;
using Product.Core.Utils;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("selling")]
        public async Task<IActionResult> GetTopUser([FromQuery] QueryObjectOrder query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _orderService.FindListUserSelling(query);

            return Ok(users);

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _orderService.CreateAsync(order);
            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}/user")]

        public async Task<IActionResult> GetOrderByUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.FindUserOrderAsync(id);
            return Ok(result);
        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetDetail(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.FindByIdAsync(id);

            if (result is null)
            {
                return NotFound("Order not found");
            }

            return Ok(result);
        }


        [HttpGet]

        public async Task<IActionResult> GetOrder()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.FindAllAsync();
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] string id, [FromBody] UpdateOrderDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.UpdateOrderAsync(id, updateDto);
            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _orderService.DeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
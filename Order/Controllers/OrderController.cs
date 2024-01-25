using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Order.core.Dtos;
using Order.core.Interfaces;
using Order.Core.Dtos;

namespace Order.Controllers
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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
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
            var result = await _orderService.GetAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetDetail(string id)
        {
            var result = await _orderService.GetDetailAsync(id);

            if (result is null)
            {
                return NotFound("Order not found");
            }

            return Ok(result);
        }

        [HttpGet]

        public async Task<IActionResult> GetOrder()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] string id, [FromBody] UpdateDto updateDto)
        {
            var result = await _orderService.UpdateAsync(id, updateDto);
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
            var result = await _orderService.DeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Order.controller
{
    [ApiController]
    [Route("api/order")]
    public class TestController : ControllerBase
    {
        [HttpGet]

        public IActionResult GetHello()
        {
            return Ok("Hello World");
        }
    }
}
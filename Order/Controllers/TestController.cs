using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Interfaces;
using Order.Core.Utils;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IGmailService _gmailService;

        public TestController(IGmailService gmailService)
        {
            _gmailService = gmailService;
        }

        [HttpGet]

        public async Task<IActionResult> SendMail()
        {
            try
            {
                MailRequest mailRequest = new()
                {
                    ToEmail = "nguyenthang240803@gmail.com",
                    Subject = "Hello World",
                    Message = "Test mail send Net"
                };
                await _gmailService.SendEmailAsync(mailRequest);
                return Ok("Mail Send Success");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Something went wrong!");
            }
        }

    }
}
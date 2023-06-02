using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectSelf.WebAPI.Common;
using PerfectSelf.WebAPI.Context;
using PerfectSelf.WebAPI.Models;
using static PerfectSelf.WebAPI.Models.User;

namespace PerfectSelf.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public EmailController(PerfectSelfContext context)
        {
            _context = context;
        }

        [HttpGet("SendVerifyCode/{email}")]
        public async Task<ActionResult> SendVerifyCode(String email)
        {
            return Ok(new {result = new { email = email, error = ""} });
        }

        [HttpGet("VerifyForResetPassword/{email}/{vcode}")]
        public async Task<ActionResult> VerifyForResetPassword(String email, String vcode)
        {
            return Ok(new { result = new { email = email, verify = true, error = "" } });
        }
    }
}

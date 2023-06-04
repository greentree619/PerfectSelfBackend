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
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace PerfectSelf.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public LogController(PerfectSelfContext context)
        {
            _context = context;
        }

        [HttpGet("{meetingUid}/{log}")]
        public async Task<IActionResult> GetAsync(String meetingUid, String log)
        {
            bool result = false;
            try
            {
                lock (Global.LockMe)
                {
                    var logMap = new LogInfo();
                    logMap.uid = meetingUid;
                    logMap.log = log;
                    Global.logQueue.Enqueue(logMap);
                    Global._canExecute.Set();
                }
                //String logFile = Directory.GetCurrentDirectory() + $"\\Log\\{meetingUid}.log";
                //String logContent = $"[{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}] {log}\n";
                //System.IO.File.AppendAllText(logFile, logContent);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(new { result=result});
        }
    }
}

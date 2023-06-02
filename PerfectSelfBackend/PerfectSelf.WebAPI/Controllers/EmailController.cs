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
    public class EmailController : ControllerBase
    {
        private readonly PerfectSelfContext _context;

        public EmailController(PerfectSelfContext context)
        {
            _context = context;
        }

        [HttpGet("SendVerifyCode/{emailAddr}")]
        public async Task<ActionResult> SendVerifyCode(String emailAddr)
        {
            String error = "";
            Random generator = new Random();
            String vcode = generator.Next(1000000, 9999999).ToString("D7");
            
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("PerfectSelf Team", "noreply@perfect-self.com"));
            email.To.Add(new MailboxAddress(emailAddr, emailAddr));

            email.Subject = "Verify code for reset password";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $@"
<table dir=""ltr"">
   <tbody>
      <tr>
         <td id=""m_-4269132539574250519i1"" style=""padding:0;font-family:'Segoe UI Semibold','Segoe UI Bold','Segoe UI','Helvetica Neue Medium',Arial,sans-serif;font-size:17px;color:#707070"">
            PerfectSelf
         </td>
      </tr>
      <tr>
         <td id=""m_-4269132539574250519i2"" style=""padding:0;font-family:'Segoe UI Light','Segoe UI','Helvetica Neue Medium',Arial,sans-serif;font-size:41px;color:#2672ec"">
            Verify code
         </td>
      </tr>
      <tr>
         <td id=""m_-4269132539574250519i3"" style=""padding:0;padding-top:25px;font-family:'Segoe UI',Tahoma,Verdana,Arial,sans-serif;font-size:14px;color:#2a2a2a"">
            Please use the following verify code for reset password.
         </td>
      </tr>
      <tr>
         <td id=""m_-4269132539574250519i4"" style=""padding:0;padding-top:25px;font-family:'Segoe UI',Tahoma,Verdana,Arial,sans-serif;font-size:14px;color:#2a2a2a"">
            Verify code: <span style=""font-family:'Segoe UI Bold','Segoe UI Semibold','Segoe UI','Helvetica Neue Medium',Arial,sans-serif;font-size:14px;font-weight:bold;color:#2a2a2a"">{vcode}</span>
         </td>
      </tr>
      <tr>
         <td id=""m_-4269132539574250519i6"" style=""padding:0;padding-top:25px;font-family:'Segoe UI',Tahoma,Verdana,Arial,sans-serif;font-size:14px;color:#2a2a2a"">
            Thanks,
         </td>
      </tr>
      <tr>
         <td id=""m_-4269132539574250519i7"" style=""padding:0;font-family:'Segoe UI',Tahoma,Verdana,Arial,sans-serif;font-size:14px;color:#2a2a2a"">
            The PerfectSelf account team
         </td>
      </tr>
   </tbody>
</table>
"
            };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("fluisotoniel@gmail.com", "kfucfusuojfwjzdk");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    Global.verifyCodeMap[emailAddr] = vcode;
                }
                catch (Exception e) {
                    error = "Failed";
                }
            }
            
            return Ok(new {result = new { email = emailAddr, error = error } });
        }

        [HttpGet("VerifyForResetPassword/{email}/{vcode}")]
        public async Task<ActionResult> VerifyForResetPassword(String email, String vcode)
        {
            bool verify = false;
            if (Global.verifyCodeMap[email] != null 
                && Global.verifyCodeMap[email].ToString().CompareTo(vcode) == 0)
            {
                Global.verifyCodeMap[email] = null;
                verify = true;
            }
            return Ok(new { result = new { email = email, verify = verify, error = "" } });
        }
    }
}

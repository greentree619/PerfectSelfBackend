using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectSelf.WebAPI.Models;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using PerfectSelf.WebAPI.Common;

namespace PerfectSelf.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        SNSClient snsClient = new SNSClient("AKIAUG22JIQEI4J44HP7"
                        , "lC1YrGkSkFfHuTwQawWENqGH9qdrBSbhNETbo1Ei"
                        , "us-east-1"
                        , "");

        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            String targetArn = "";
            String err = "";
            PublishResponse response = null;
            try
            {
                targetArn = await snsClient.GetTargetArn(notificationModel.DeviceId
                    , "arn:aws:sns:us-east-1:289562772488:app/APNS/PerfectSelfApp-Live");
                response = await snsClient.SendNotification(targetArn, "You received booking invitation.", "PerfectSelf", "");
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            
            return Ok(new { result = (response != null), error = err});
        }

        [Route("sendForDev")]
        [HttpPost]
        public async Task<IActionResult> SendNotificationForDev(NotificationModel notificationModel)
        {
            String targetArn = "";
            String err = "";
            PublishResponse response = null;
            try
            {
                targetArn = await snsClient.GetTargetArn(notificationModel.DeviceId
                    , "arn:aws:sns:us-east-1:289562772488:app/APNS_SANDBOX/PerfectSelfApp");

                response = await snsClient.SendNotification(targetArn, "You received booking invitation.", "PerfectSelf", "_SANDBOX");
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }


            return Ok(new { result = (response != null), error = err });
        }
    }
}

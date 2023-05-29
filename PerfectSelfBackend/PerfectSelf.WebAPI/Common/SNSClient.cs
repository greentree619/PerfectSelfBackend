using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PerfectSelf.WebAPI.Common
{
    public class SNSClient
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _region;
        private readonly string _topicArn;
        private AmazonSimpleNotificationServiceClient client;

        public SNSClient(string accessKey, string secretKey, string region, string topicArn)
        {
            _accessKey = accessKey;
            _secretKey = secretKey;
            _region = region;
            _topicArn = topicArn;
            client = new AmazonSimpleNotificationServiceClient(_accessKey, _secretKey, Amazon.RegionEndpoint.GetBySystemName(_region));
        }

        public async Task PublishNotificationAsync(string message, string deviceId)
        {
            var messageAttributes = new Dictionary<string, MessageAttributeValue>();
            messageAttributes.Add("AWS.SNS.MOBILE.APNS.PRIORITY", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = "10"
            });

            var request = new PublishRequest
            {
                TargetArn = _topicArn,
                MessageStructure = "json",
                Message = "{\"default\":\"" + message + "\",\"APNS_SANDBOX\":\"{\\\"aps\\\":{\\\"alert\\\":\\\"" + message + "\\\"}}\"}",
                MessageAttributes = messageAttributes
            };

            var result = await client.PublishAsync(request);

            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Notification sent to device with device id: " + deviceId);
            }
            else
            {
                Console.WriteLine("Failed to send notification to device with device id: " + deviceId);
            }
        }

        public async Task<PublishResponse> SendNotification(string targetArn, string message, string subject)
        {
            PublishResponse response = null;
            var request = new PublishRequest
            {
                TargetArn = targetArn, // replace with your target ARN
                MessageStructure = "json",
                Message = $"{{\"APNS_SANDBOX\":\"{{\\\"aps\\\":{{\\\"alert\\\":\\\"{message}\\\"}}}}\"}}"
            };

            try
            {
                response = await client.PublishAsync(request);
            }
            catch (Exception ex)
            {
                response = null;
            }
            //Console.WriteLine("Message sent to device with token {0}", deviceToken);
            return response;
        }

        public async Task<string> GetTargetArn(string deviceToken, string platformApplicationArn)
        {
            var request = new CreatePlatformEndpointRequest
            {
                PlatformApplicationArn = platformApplicationArn,
                Token = deviceToken
            };

            var response = await client.CreatePlatformEndpointAsync(request);

            return response.EndpointArn;
        }
    }
}

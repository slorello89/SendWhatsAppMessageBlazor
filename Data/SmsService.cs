using Microsoft.Extensions.Configuration;
using Nexmo.Api.Messaging;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerHelloWorld.Data
{
    public class SmsService
    {
        private readonly IConfiguration _config;
        public SmsService(IConfiguration config)
        {
            _config = config;
        }

        public SendSmsResponse SendSms(string to, string from, string text)
        {
            var apiKey = _config["VONAGE_API_KEY"];
            var apiSecret = _config["VONAGE_API_SECRET"];            
            var creds = Credentials.FromApiKeyAndSecret(apiKey, apiSecret);
            var client = new SmsClient(creds);
            var request = new SendSmsRequest
            {
                To = to,
                From = from,
                Text = text
            };
            return client.SendAnSms(request);
        }
    }
}

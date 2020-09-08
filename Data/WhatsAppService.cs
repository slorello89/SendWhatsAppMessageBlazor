using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerHelloWorld.Data
{
    public class WhatsAppService
    {
        private readonly IConfiguration _config;

        public WhatsAppService(IConfiguration config)
        {
            _config = config;
        }
        public string SendWhatsAppMessage(string toNum, string fromNum, string text){
            var appId = _config["APP_ID"];
            var privateKey = _config["privateKeyPath"];
            var content = new { type = "text", text };
            var message = new { content };
            var to = new { type = "whatsapp", number = toNum };
            var from = new { type = "whatsapp", number = fromNum };
            var creds = Credentials.FromAppIdAndPrivateKeyPath(appId, privateKey);
            var uri = new Uri("https://messages-sandbox.nexmo.com/v0.1/messages");
            var request = new { to, from, message };
            var response = ApiRequest.DoRequestWithJsonContent<JObject>
                ("POST", uri, request, ApiRequest.AuthType.Bearer, creds);
            return response["message_uuid"].ToString();
        }
    }
}

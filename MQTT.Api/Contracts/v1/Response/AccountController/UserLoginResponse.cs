using System;

namespace MQTT.Api.Contracts.v1.Response.AccountController
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public DateTime NowBefore { get; set; }
        public DateTime Expires { get; set; }
    }
}
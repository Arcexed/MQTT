namespace MQTT.Api.Options
{
    public class ApiRoutes
        {
            public const string Root = "api";
            public const string Version = "v1";
            public const string Base = Root + "/" + Version;
    
            public static class Devices
            {
                public const string GetAll = Base + "/device";
                public const string Update = Base + "/device/{deviceId}";
                public const string Delete = Base + "/device/{deviceId}";
                public const string Get = Base + "/device/{deviceId}";
                public const string Create = Base + "/device";
            }
            public static class Measurements
            {
                public const string Get = Base + "/measurements/";
                public const string Create = Base + "/measurements/";
            }
            public static class Reserve
            {
                public const string GetAll = Base + "/posts";
                public const string Update = Base + "/posts/{postId}";
                public const string Delete = Base + "/posts/{postId}";
                public const string Get = Base + "/posts/{postId}";
                public const string Create = Base + "/posts/";
            }

            public static class Account
            {
                public const string Login = Base + "/account/login";
                public const string Register = Base + "/account/register";
                public const string Refresh = Base + "/account/refresh";
            }
        }
}
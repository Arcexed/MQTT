namespace MQTT.Api.Options
{
    public class ApiRoutes
        {
            public const string Root = "api";
            public const string Version = "v1";
            public const string Base = Root + "/" + Version;
    
            public static class Device
            {
                public const string GetAll = Base + "/device";
                public const string Update = Base + "/device/{DeviceId}";
                public const string Delete = Base + "/device/{DeviceId}";
                public const string Get = Base + "/device/{DeviceId}";
                public const string Create = Base + "/device";
            }
            public static class Measurements
            {
                public const string Get = Base + "/measurements/{DeviceId}";
                public const string Create = Base + "/measurements/{DeviceId}";
            }
            public static class Reserve
            {
                public const string GetAll = Base + "/posts";
                public const string Update = Base + "/posts/{postId}";
                public const string Delete = Base + "/posts/{postId}";
                public const string Get = Base + "/posts/{postId}";
                public const string Create = Base + "/posts/";
            }
        }
}
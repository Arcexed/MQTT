using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Models.DbModels;

namespace MQTTWebApi.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "MQTT_SERVER"; // издатель токена
        public const string AUDIENCE = "MQTT_API_Client"; // потребитель токена
        const string KEY = "SECRET_KEY_VERY_HASHED1234!@#$";   // ключ для шифрации
        public const int LIFETIME = 15; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }

}
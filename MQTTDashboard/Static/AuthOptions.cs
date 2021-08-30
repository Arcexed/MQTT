using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace MQTTDashboard.Static
{
    public class AuthOptions
    {
        public const string ISSUER = "MQTT_Server"; // издатель токена
        public const string AUDIENCE = "FOR_MQTT_USERS_DASHBOARD"; // потребитель токена
        const string KEY = "-cK3j6554Z%3@((Kug^Pb73UtHmT;hC@";   // ключ для шифрации
        public const int LIFETIME = 120; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

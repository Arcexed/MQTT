using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MQTT.Api.Auth
{
    public static class AuthOptions
    {
        public const string Issuer = "MQTT_SERVER"; // издатель токена
        public const string Audience = "MQTT_API_Client"; // потребитель токена
        private const string Key = "SECRET_KEY_VERY_HASHED1234!@#$"; // ключ для шифрации
        public const int Lifetime = 15; // время жизни токена - 15 минут (15?)

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
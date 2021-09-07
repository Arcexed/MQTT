using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.DbModels;

namespace MQTTWebApi.Auth
{
    public class Auth
    {
        private readonly mqttdb_newContext _db;

        public Auth(mqttdb_newContext db)
        {
            _db = db;
        }

        public User FindUser(string token)
        {
            return _db.Users.FirstOrDefault(d => d.AccessToken == token);
        }

    }
}

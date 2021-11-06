using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQTT.Data;
using MQTT.Data.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace MQTT.Api.Controllers.Api
{
    public class ServiceController : ControllerBase
    {
        private readonly MQTTDbContext _db;
        public ServiceController(MQTTDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("database add some changes")]
        [Route("database")]
        public async Task<IActionResult> Database()
        {
            _db.Roles.Add(new Role()
            {
                Id = Guid.NewGuid(),
                Name = "User"
            });
            await _db.SaveChangesAsync();
            _db.Users.Add(new User()
            {
                Id = Guid.NewGuid(),
                Email = "arcex.off@gmail.com",
                Ip = "",
                Password = "test",
                Role = await _db.Roles.FirstAsync(d=>d.Name=="User"),
                AccessToken = "5356cf8159584b31864e1c8ba72232cb",
                IsBlock = false,
                Username = "Arcex",
            });
            await _db.SaveChangesAsync();
            _db.Devices.Add(new Device()
            {
                Id = Guid.NewGuid(),
                Description = "Device for testing system",
                Geo = "Lomonosova, 67",
                Name = "TestDevice",
                CreatingDate = DateTime.Now,
                IsPublic = false,
                MqttToken = "3db1f91c8ba1475fb24c3c0ce62e1415",
                User = await _db.Users.FirstAsync(d=>d.Username=="Arcex")
            });
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("database add some changes")]
        [Route("gc")]
        public async Task<IActionResult> GarbageCollector()
        {
            Dictionary<string, string> dictionary = new();
            dictionary.Add("GC.MaxGeneration",GC.MaxGeneration.ToString());
            dictionary.Add("GetTotalMemory",GC.GetTotalMemory(true).ToString());
            dictionary.Add("GetTotalAllocatedBytes",GC.GetTotalAllocatedBytes().ToString());
            string result = "";
            foreach (var keyPair in dictionary)
            {
                result += $"{keyPair.Key} {keyPair.Value}\n";
            }

            return Ok(result);
        }

    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MQTT.Data
{
    public class MqttContextFactory : IDesignTimeDbContextFactory<MQTTDbContext>
    {
        public MQTTDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MQTTDbContext>();
            optionsBuilder.UseSqlServer(
                "Data Source=178.54.86.113,14330;Initial Catalog=mqttdb_new;User ID=SA;Password=19Andrei19");
            return new MQTTDbContext(optionsBuilder.Options);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UnitTests
{
    class Program
    {
        public static void Main(string[] args)
        {

            int count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                using (WebClient webClient = new WebClient())
                {
                    Random rnd = new Random();
                    string deviceName = "TestDevice";
                    float atmosphericPressure = rnd.Next(650, 850);
                    float temperature = rnd.Next(15, 35);
                    float airHumidity = rnd.Next(15, 80);
                    float lightLevel = rnd.Next(15, 30);
                    float smokeLevel = rnd.Next(15, 50);
                    var builder = new UriBuilder($"https://localhost:44395/api/Measurements/{deviceName}/Add");
                    builder.Port = 44395;
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["atmosphericPressure"] = atmosphericPressure.ToString();
                    query["temperature"] = temperature.ToString();
                    query["airHumidity"] = airHumidity.ToString();
                    query["lightLevel"] = lightLevel.ToString();
                    query["smokeLevel"] = smokeLevel.ToString();
                    builder.Query = query.ToString();
                    string url = builder.ToString();
                    webClient.DownloadString(url);
                }
            }


        }
    }
}

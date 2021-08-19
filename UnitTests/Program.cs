using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace UnitTests
{
    class Program
    {
        public static void Main(string[] args)
        {



            using (WebClient webClient = new WebClient())
            {
                int i = 1;
                while (true)
                {
                    Random rnd = new Random();
                    string deviceName = "TestName";
                    float atmosphericPressure = rnd.Next(650, 850);
                    float temperature = rnd.Next(15, 35);
                    float airHumidity = rnd.Next(15, 80);
                    float lightLevel = rnd.Next(15, 30);
                    float smokeLevel = rnd.Next(15, 50);
                    var builder = new UriBuilder($"https://localhost:44395/Measurements/{deviceName}/Add");
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
                    Console.WriteLine(
                        $"{i} {deviceName} {DateTime.Now.ToString("G")} {atmosphericPressure} {temperature} {airHumidity} {lightLevel} {smokeLevel}");
                    i++;
                    Thread.Sleep(1000);
                }
            }



        }
    }
}
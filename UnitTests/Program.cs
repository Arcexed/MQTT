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
            for (int i = 0; i < 50000; i++)
            {
                
                    Task.Run(() => Generate());
                    counter++;
                
            }
            

            Console.ReadKey();
        }

        public static int counter = 0;
        static async Task Generate()
        {
            using (WebClient webClient = new WebClient())
            {
                Random rnd = new Random();
                string deviceName = "TestNameAndrey";
                float atmosphericPressure = rnd.Next(650, 850);
                float temperature = rnd.Next(15, 35);
                float airHumidity = rnd.Next(15, 80);
                float lightLevel = rnd.Next(15, 30);
                float smokeLevel = rnd.Next(15, 50);
                try
                {
                     //var builder = new UriBuilder($"http://178.54.86.113/Measurements/{deviceName}/Add");
                    var builder = new UriBuilder($"http://192.168.3.160/Measurements/{deviceName}/Add");
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["atmosphericPressure"] = atmosphericPressure.ToString();
                    query["temperature"] = temperature.ToString();
                    query["airHumidity"] = airHumidity.ToString();
                    query["lightLevel"] = lightLevel.ToString();
                    query["smokeLevel"] = smokeLevel.ToString();
                    builder.Query = query.ToString();
                    string url = builder.ToString();
                    await webClient.DownloadStringTaskAsync(builder.Uri);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                Console.WriteLine($"{counter} {deviceName} {DateTime.Now.ToString("G")} {atmosphericPressure} {temperature} {airHumidity} {lightLevel} {smokeLevel}");
                // Thread.Sleep(1000);

            }
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace StreamAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                        .UseUrls("http://localhost:5555")
                        .Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }
             );
    }
}

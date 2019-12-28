using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace sl.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "service-log";
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(config =>
                    {
                        config.AddConsole();
                        config.AddEventLog();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}

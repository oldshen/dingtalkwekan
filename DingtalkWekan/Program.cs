using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DingtalkWekan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             //.ConfigureLogging(logging =>
             //{
             //    // logging.ClearProviders();//去掉默认添加的日志提供程序

             //    logging.AddConsole();
             //    logging.AddDebug();
             //    logging.AddEventSourceLogger();

             //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    
                   
                    .UseStartup<Startup>();
                });
    }
}

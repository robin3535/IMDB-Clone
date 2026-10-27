using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ImdbApi.Migration;

namespace ImdbApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.Services.GetRequiredService<DatabaseInitializer>().InitializeAsync();
            await host.RunAsync();
        }

        //to configure and create an instance of an ASP.NET Core web host.
        //The web host is responsible for managing the application's lifetime,
        //handling incoming HTTP requests, and routing them to the appropriate controllers and actions.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

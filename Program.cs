﻿using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace IdentityServerWithAspNetIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServer";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .CaptureStartupErrors(true)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

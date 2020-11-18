using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using sl.web;

Console.Title = "service-log";
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    }).Build().Run();

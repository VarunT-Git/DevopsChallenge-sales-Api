using DevOpsChallenge.SalesApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

// Set console title
Console.Title = "DevOps Challenge Sales API";

// Build and run host
await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddEnvironmentVariables("APP_");
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.UseKestrel((options) =>
        {
            //Turn off the server details in the repsonse header
            options.AddServerHeader = false;
        });
    })
    .RunConsoleAsync();
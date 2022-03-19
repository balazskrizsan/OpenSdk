using System;
using Microsoft.Extensions.Hosting;
using OpenSdk.Registries;
using Serilog;

namespace OpenSdk.IntegrationTest;

public class AbstractIntegrationTest
{
    public IServiceProvider GetServices(ApplicationArgumentRegistry? applicationArgumentRegistry = null)
    {
        var defaultApplicationArgumentRegistry =
            applicationArgumentRegistry ?? new ApplicationArgumentRegistry("", "");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services
                    .ConfigureDependencies(defaultApplicationArgumentRegistry)
                    .SetupLogger();
            })
            .UseSerilog()
            .Build();

        return host.Services;
    }
}
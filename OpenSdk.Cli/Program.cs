using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenSdk.Registries;
using Serilog;

namespace OpenSdk
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicationArgumentRegistry = new ApplicationArgumentRegistry(
                args[0],
                args[1],
                args[2]
            );

            var host = AppStartup(applicationArgumentRegistry);

            ActivatorUtilities.CreateInstance<CliBootstrap>(host.Services).Start();
        }

        static IHost AppStartup(IApplicationArgumentRegistry applicationArgumentRegistry)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services
                        .ConfigureDependencies(applicationArgumentRegistry)
                        .SetupLogger();
                })
                .UseSerilog()
                .Build();
        }
    }
}
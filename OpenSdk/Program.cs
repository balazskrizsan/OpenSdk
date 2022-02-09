using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace OpenSdk
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataSourcePath = args[0];
            var host = AppStartup(dataSourcePath);

            var bootstrap = ActivatorUtilities.CreateInstance<Bootstrap>(host.Services);

            bootstrap.Start();
        }

        static IHost AppStartup(string dataSourcePath)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services
                        .ConfigureDependencies(dataSourcePath)
                        .SetupLogger();
                })
                .UseSerilog()
                .Build();

            return host;
        }
    }
}

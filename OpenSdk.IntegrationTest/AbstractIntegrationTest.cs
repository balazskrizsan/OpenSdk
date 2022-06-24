using System;
using Microsoft.Extensions.Hosting;
using OpenSdk.Registries;
using Serilog;

namespace OpenSdk.IntegrationTest;

public class AbstractIntegrationTest
{
    public static string INTEGRATION_ROOT_FOLDER = $"{Environment.CurrentDirectory}\\..\\..\\..";

    public static Func<string, string, ApplicationArgumentRegistry> APP_ARGS_PRESET_JAVA =
        (inputFilePath, testFullAppOutput) => new ApplicationArgumentRegistry(
            inputFilePath,
            testFullAppOutput,
            "stackjudge_aws_sdk",
            "Java"
        );

    public static Func<string, string, ApplicationArgumentRegistry> APP_ARGS_PRESET_TYPESCRITP =
        (inputFilePath, testFullAppOutput) => new ApplicationArgumentRegistry(
            inputFilePath,
            testFullAppOutput,
            "stackjudge-frontend-sdk",
            "TypeScript"
        );

    public IServiceProvider GetServices(ApplicationArgumentRegistry applicationArgumentRegistry)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services
                    .ConfigureDependencies(applicationArgumentRegistry)
                    .SetupLogger();
            })
            .UseSerilog()
            .Build();

        return host.Services;
    }
}
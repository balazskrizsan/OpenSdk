using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSdk.Factories;
using OpenSdk.Registries;
using OpenSdk.Services;
using OpenSdk.Services.ParserServices;
using Serilog;

namespace OpenSdk
{
    public static class ConfigureServicesHelperExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection, string dataSourcePath)
        {
            return serviceCollection
                .AddLogging()
                .AddSingleton<IApplicationArgumentRegistry>(_ => new ApplicationArgumentRegistry(dataSourcePath))
                .AddSingleton<IBootstrap, Bootstrap>()
                .AddSingleton<IParserService, ParserService>()
                .AddSingleton<IComponentsParserService, ComponentsParserService>()
                .AddSingleton<IPathsParserService, PathsParserService>()
                .AddSingleton<IGeneratorService, GeneratorService>()
                .AddSingleton<ICottleFactory, CottleFactory>();
        }

        public static IServiceCollection SetupLogger(this IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
            
            Log.Logger = new LoggerConfiguration() // initiate the logger configuration
                .ReadFrom.Configuration(configuration) // connect serilog to our configuration folder
                .Enrich.FromLogContext() //Adds more information to our logs from built in Serilog 
                .WriteTo.Console() // decide where the logs are going to be shown
                .CreateLogger();


            return serviceCollection;
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSdk.Factories;
using OpenSdk.Registries;
using OpenSdk.Services.Filters;
using OpenSdk.Services;
using OpenSdk.Services.ParserServices;
using Serilog;

namespace OpenSdk;

public static class ConfigureServicesHelperExtensions
{
    public static IServiceCollection ConfigureDependencies(
        this IServiceCollection serviceCollection,
        IApplicationArgumentRegistry applicationArgumentRegistry
    )
    {
        return serviceCollection
                .AddLogging()
                .AddSingleton(_ => applicationArgumentRegistry)
                .AddSingleton<ICliBootstrap, CliBootstrap>()
                .AddSingleton<IParserService, ParserService>()
                .AddSingleton<IComponentsParserService, ComponentsParserService>()
                .AddSingleton<IPathsParserService, PathsParserService>()
                .AddSingleton<IGeneratorService, GeneratorService>()
                .AddSingleton<IMapperService, MapperService>()
                .AddSingleton<IFileService, FileService>()
                .AddSingleton<IInterfaceGeneratorService, InterfaceGeneratorService>()
                .AddSingleton<IValueObjectGeneratorService, ValueObjectGeneratorService>()
                .AddSingleton<ILanguageSpecificGeneratorService, LanguageSpecificGeneratorService>()
                .AddSingleton<ITemplateService, TemplateService>()
                .AddSingleton<IValueObjectByMethodTypeFilterService, ValueObjectByMethodTypeFilterService>()
                .AddSingleton<IDotLiquidFactory, DotLiquidFactory>()
            ;
    }

    public static IServiceCollection SetupLogger(this IServiceCollection serviceCollection)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();

        serviceCollection.AddSingleton(configuration);

        Log.Logger = new LoggerConfiguration() // initiate the logger configuration
            .ReadFrom.Configuration(configuration) // connect serilog to our configuration folder
            .Enrich.FromLogContext() //Adds more information to our logs from built in Serilog 
            .WriteTo.Console() // decide where the logs are going to be shown
            .CreateLogger();

        return serviceCollection;
    }
}
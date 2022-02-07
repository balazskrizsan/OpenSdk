using Microsoft.Extensions.DependencyInjection;
using OpenSdk.Factories;
using OpenSdk.Registries;
using OpenSdk.Services;
using OpenSdk.Services.ParserServices;

namespace OpenSdk
{
    public static class ConfigureServicesHelperExtensions
    {
        public static IServiceCollection Configure(this IServiceCollection serviceCollection, string dataSourcePath)
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
    }
}
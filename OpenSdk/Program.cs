using Microsoft.Extensions.DependencyInjection;
using OpenSdk.Factories;
using OpenSdk.Registries;
using OpenSdk.Services;
using OpenSdk.Services.ParserServices;

namespace OpenSdk
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataSourcePath = args[0];

            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IApplicationArgumentRegistry>(_ => new ApplicationArgumentRegistry(dataSourcePath))
                .AddSingleton<IBootstrap, Bootstrap>()
                .AddSingleton<IParserService, ParserService>()
                .AddSingleton<IComponentsParserService, ComponentsParserService>()
                .AddSingleton<IPathsParserService, PathsParserService>()
                .AddSingleton<IGeneratorService, GeneratorService>()
                .AddSingleton<ICottleFactory, CottleFactory>()
                .BuildServiceProvider();

            serviceProvider.GetService<IBootstrap>().Start();
        }
    }
}

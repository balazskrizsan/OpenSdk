using OpenSdk.Registries;
using OpenSdk.Services;
using OpenSdk.ValueObjects;

namespace OpenSdk
{
    public class Bootstrap : IBootstrap
    {
        private readonly IParserService parserService;
        private readonly IGeneratorService generatorService;
        private readonly ApplicationArgumentRegistry applicationArgumentRegistry;

        public Bootstrap(
            IParserService parserService,
            IGeneratorService generatorService,
            IApplicationArgumentRegistry applicationArgumentRegistry
        )
        {
            this.parserService = parserService;
            this.generatorService = generatorService;
            this.applicationArgumentRegistry = applicationArgumentRegistry as ApplicationArgumentRegistry;
        }

        public void Start()
        {
            ParserResponse parserResponse = parserService.Parse(applicationArgumentRegistry.DataSourcePath);

            generatorService.Generate(parserResponse);
        }
    }
}
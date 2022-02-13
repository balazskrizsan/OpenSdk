using OpenSdk.Registries;
using OpenSdk.Services;

namespace OpenSdk
{
    public class Bootstrap : IBootstrap
    {
        private readonly IParserService parserService;
        private readonly IGeneratorService generatorService;
        private readonly IApplicationArgumentRegistry applicationArgumentRegistry;

        public Bootstrap(
            IParserService parserService,
            IGeneratorService generatorService,
            IApplicationArgumentRegistry applicationArgumentRegistry
        )
        {
            this.parserService = parserService;
            this.generatorService = generatorService;
            this.applicationArgumentRegistry = applicationArgumentRegistry;
        }

        public void Start()
        {
            var parserResponse = parserService.Parse(applicationArgumentRegistry.DataSourcePath);

            generatorService.Generate(parserResponse);
        }
    }
}
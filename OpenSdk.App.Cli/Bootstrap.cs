using Microsoft.Extensions.Logging;
using OpenSdk.Registries;
using OpenSdk.Services;

namespace OpenSdk.App.Cli
{
    public class Bootstrap : IBootstrap
    {
        private readonly IParserService parserService;
        private readonly IGeneratorService generatorService;
        private readonly IApplicationArgumentRegistry applicationArgumentRegistry;
        private readonly ILogger<ParserService> logger;

        public Bootstrap(
            IParserService parserService,
            IGeneratorService generatorService,
            IApplicationArgumentRegistry applicationArgumentRegistry,
            ILogger<ParserService> logger
        )
        {
            this.parserService = parserService;
            this.generatorService = generatorService;
            this.applicationArgumentRegistry = applicationArgumentRegistry;
            this.logger = logger;
        }

        public void Start()
        {
            var parserResponse = parserService.Parse(applicationArgumentRegistry.DataSourcePath);

            generatorService.Generate(parserResponse);

            logger.LogInformation("====== Successful finish");
        }
    }
}
using Microsoft.Extensions.Logging;
using OpenSdk.Services.GeneratorServices;
using OpenSdk.ValueObjects;

namespace OpenSdk.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ILogger<ParserService> logger;
        private readonly IInterfaceGeneratorService interfaceGeneratorService;
        private readonly IValueObjectGeneratorService valueObjectGeneratorService;

        public GeneratorService(
            ILogger<ParserService> logger,
            IInterfaceGeneratorService interfaceGeneratorService,
            IValueObjectGeneratorService valueObjectGeneratorService
        )
        {
            this.logger = logger;
            this.interfaceGeneratorService = interfaceGeneratorService;
            this.valueObjectGeneratorService = valueObjectGeneratorService;
        }

        public void Generate(ParserResponse openapiValues)
        {
            logger.LogInformation("====== Generate API Interfaces");
            interfaceGeneratorService.Generate(openapiValues.Methods);

            logger.LogInformation("====== Generate Value Objects");
            valueObjectGeneratorService.Generate(openapiValues.Schemas);
        }
    }
}

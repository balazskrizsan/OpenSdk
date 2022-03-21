using System.Collections.Generic;
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
        private readonly IFileService fileService;

        public GeneratorService(
            ILogger<ParserService> logger,
            IInterfaceGeneratorService interfaceGeneratorService,
            IValueObjectGeneratorService valueObjectGeneratorService,
            IFileService fileService
        )
        {
            this.logger = logger;
            this.interfaceGeneratorService = interfaceGeneratorService;
            this.valueObjectGeneratorService = valueObjectGeneratorService;
            this.fileService = fileService;
        }

        public void Generate(ParserResponse openapiValues)
        {
            var files = new List<File>();

            logger.LogInformation("====== Generate API Interfaces");
            files.AddRange(interfaceGeneratorService.GenerateFiles(openapiValues.Methods));

            logger.LogInformation("====== Generate Value Objects");
            files.AddRange(valueObjectGeneratorService.GetGeneratedFiles(openapiValues.Schemas));

            logger.LogInformation("====== Saving generated files");
            fileService.SaveFiles(files);
        }
    }
}
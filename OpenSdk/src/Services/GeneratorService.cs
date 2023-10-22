using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenSdk.Services.GeneratorServices;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

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
            files.AddRange(interfaceGeneratorService.GetGenerateFiles(openapiValues.UriMethods));

            logger.LogInformation("====== Generate PropertyValue Objects");
            files.AddRange(valueObjectGeneratorService.GetGeneratedFiles(openapiValues.Schemas));

            var customSchemas = openapiValues.UriMethods
                .FindAll(m => m.GetMethod is { CustomSchema: not null })
                .Select(m => Map(m.GetMethod.CustomSchema))
                .ToList();
            files.AddRange(valueObjectGeneratorService.GetGeneratedFiles(customSchemas));

            logger.LogInformation("====== Saving generated files");
            fileService.SaveFilesAsync(files);
        }

        private Schema Map(CustomSchema customSchema)
        {
            return new Schema(customSchema.ClassName, "custom", customSchema.Parameters, true, false);
        }
    }
}
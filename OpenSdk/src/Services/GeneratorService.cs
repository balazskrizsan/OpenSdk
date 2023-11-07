using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;

namespace OpenSdk.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ILogger<ParserService> logger;
        private readonly IInterfaceGeneratorService interfaceGeneratorService;
        private readonly IValueObjectGeneratorService valueObjectGeneratorService;
        private readonly ILanguageSpecificGeneratorService languageSpecificGeneratorService;
        private readonly IFileService fileService;

        public GeneratorService(
            ILogger<ParserService> logger,
            IInterfaceGeneratorService interfaceGeneratorService,
            IValueObjectGeneratorService valueObjectGeneratorService,
            ILanguageSpecificGeneratorService languageSpecificGeneratorService,
            IFileService fileService
        )
        {
            this.logger = logger;
            this.interfaceGeneratorService = interfaceGeneratorService;
            this.valueObjectGeneratorService = valueObjectGeneratorService;
            this.languageSpecificGeneratorService = languageSpecificGeneratorService;
            this.fileService = fileService;
        }

        public void Generate(ParserResponse openapiValues)
        {
            var files = new List<File>();

            files = GenerateInterfaces(files, openapiValues.UriMethods);
            files = GenerateValueObjects(files, openapiValues.Schemas);
            files = GenerateCustomValueObjects(files, openapiValues.UriMethods);
            files = GenerateLanguageSpecificFiles(files);

            logger.LogInformation("====== Saving generated files");
            fileService.SaveFilesAsync(files);
        }

        private List<File> GenerateInterfaces(List<File> files, List<UriMethods> uriMethods)
        {
            logger.LogInformation("====== Generate interfaces");
            files.AddRange(interfaceGeneratorService.GetGenerateFiles(uriMethods));

            return files;
        }

        private List<File> GenerateValueObjects(List<File> files, List<Schema> schemas)
        {
            logger.LogInformation("====== Generate schema files");
            files.AddRange(valueObjectGeneratorService.GetGeneratedFiles(schemas));

            return files;
        }

        private List<File> GenerateCustomValueObjects(List<File> files, List<UriMethods> uriMethods)
        {
            logger.LogInformation("====== Generate custom GET schema files");
            var customPostSchemas = uriMethods
                .FindAll(m => m.PostMethod is { CustomSchema: not null })
                .Select(m => Map(m.PostMethod.CustomSchema, false, true))
                .ToList();
            files.AddRange(valueObjectGeneratorService.GetGeneratedFiles(customPostSchemas));

            logger.LogInformation("====== Generate custom POST schema files");
            var customGetSchemas = uriMethods
                .FindAll(m => m.GetMethod is { CustomSchema: not null })
                .Select(m => Map(m.GetMethod.CustomSchema, true, false))
                .ToList();
            files.AddRange(valueObjectGeneratorService.GetGeneratedFiles(customGetSchemas));

            return files;
        }

        private List<File> GenerateLanguageSpecificFiles(List<File> files)
        {
            logger.LogInformation("====== Generate language specific files");
            files.AddRange(languageSpecificGeneratorService.GetSpecificFilesByFiles(files));

            return files;
        }

        private Schema Map(CustomSchema customSchema, bool hasGet, bool hasPost)
        {
            return new Schema(customSchema.ClassName, "custom", customSchema.Parameters, hasGet, hasPost);
        }
    }
}
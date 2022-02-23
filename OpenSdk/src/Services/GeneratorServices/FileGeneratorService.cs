using System;
using System.IO;
using Microsoft.Extensions.Logging;
using OpenSdk.Registries;

namespace OpenSdk.Services.GeneratorServices
{
    public class FileGeneratorService : IFileGeneratorService
    {
        private readonly ILogger<ParserService> logger;
        private readonly IApplicationArgumentRegistry applicationArgumentRegistry;

        public FileGeneratorService(
            ILogger<ParserService> logger,
            IApplicationArgumentRegistry applicationArgumentRegistry
        )
        {
            this.logger = logger;
            this.applicationArgumentRegistry = applicationArgumentRegistry;
        }

        public void SaveFile(string destinationFolder, string fileName, string content)
        {
            destinationFolder = applicationArgumentRegistry.OutputFolder + destinationFolder;
            Directory.CreateDirectory(destinationFolder);
            File.WriteAllTextAsync(destinationFolder + "/" + fileName, content);
            logger.LogInformation("    - " + destinationFolder + "/" + fileName);
        }

        public string TypeMapper(string openapiType)
        {
            switch (openapiType)
            {
                case "string":
                    return "String";
                case "#/components/schemas/FileUpload":
                    return "HttpEntity<ByteArrayResource>";
                default:
                    throw new Exception("No type found for: " + openapiType);
            }
        }

        public string VarNameMapper(string varName)
        {
            switch (varName)
            {
                case "#/components/schemas/FileUpload":
                    return "content";
                default:
                    return varName;
            }
        }
    }
}
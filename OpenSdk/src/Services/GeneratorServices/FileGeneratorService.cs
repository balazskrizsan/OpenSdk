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
                case "boolean":
                    return "Boolean";
                case "integer":
                    return "Integer";
                case "#/components/schemas/FileUpload":
                    return "HttpEntity<ByteArrayResource>";
                default:
                    try
                    {
                        return openapiType.Split("/")[3];
                    }
                    catch (Exception)
                    {
                        throw new Exception("No type found for: " + openapiType);
                    }
            }
        }

        public string VarNameMapper(string varName)
        {
            switch (varName)
            {
                case "#/components/schemas/FileUpload":
                    return "content";
                default:
                    try
                    {
                        return varName.Split("/")[3];
                    }
                    catch (Exception)
                    {
                        return varName;
                    }
            }
        }
    }
}
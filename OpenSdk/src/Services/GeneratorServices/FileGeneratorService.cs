using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace OpenSdk.Services.GeneratorServices
{
    public class FileGeneratorService : IFileGeneratorService
    {
        private readonly ILogger<ParserService> logger;

        public FileGeneratorService(ILogger<ParserService> logger)
        {
            this.logger = logger;
        }

        public void SaveFile(string destinationFolder, string fileName, string content)
        {
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

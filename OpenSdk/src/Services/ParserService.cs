using System;
using System.IO;
using Microsoft.Extensions.Logging;
using OpenSdk.Exceptions;
using OpenSdk.Services.ParserServices;
using OpenSdk.ValueObjects;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using File = System.IO.File;

namespace OpenSdk.Services
{
    public class ParserService : IParserService
    {
        private readonly IPathsParserService pathsParserService;
        private readonly IComponentsParserService componentsParserService;
        private readonly ILogger<ParserService> logger;

        public ParserService(
            IPathsParserService pathsParserService,
            IComponentsParserService componentsParserService,
            ILogger<ParserService> logger
        )
        {
            this.logger = logger;
            this.pathsParserService = pathsParserService;
            this.componentsParserService = componentsParserService;
        }

        public ParserResponse Parse(string dataSourcePath)
        {
            logger.LogInformation("====== Parser init with {dataSourcePath}", dataSourcePath);
            StringReader input;
            try
            {
                input = new StringReader(File.ReadAllText(dataSourcePath));
            }
            catch (Exception e)
            {
                throw new ParserException($"Input read error: {e.Message}");
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            logger.LogInformation("====== Deserializing");
            var root = deserializer.Deserialize<Root>(input);

            logger.LogInformation("====== API info");
            logger.LogInformation("    {rootOpenapi}", root.Openapi);
            logger.LogInformation("    {rootInfoVersion}",  root.Info.Version);
            logger.LogInformation("    {rootInfoTitle}", root.Info.Title);

            logger.LogInformation("====== Paths parsing");
            var generatorMethods = pathsParserService.GetParsedPaths(root.Paths, root.Components);
            logger.LogInformation("====== Components parsing");
            var generatorSchemas = componentsParserService.GetParsedComponents(root.Components);

            return new ParserResponse(generatorMethods, generatorSchemas);
        }
    }
}

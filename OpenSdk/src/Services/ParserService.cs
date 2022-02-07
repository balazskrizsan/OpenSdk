using System;
using System.IO;
using OpenSdk.Services.ParserServices;
using OpenSdk.ValueObjects;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace OpenSdk.Services
{
    public class ParserService : IParserService
    {
        private readonly IPathsParserService pathsParserService;
        private readonly IComponentsParserService componentsParserService;

        public ParserService(
            IPathsParserService pathsParserService,
            IComponentsParserService componentsParserService
        )
        {
            this.pathsParserService = pathsParserService;
            this.componentsParserService = componentsParserService;
        }

        public ParserResponse Parse(string dataSourcePath)
        {
            Console.WriteLine("====== Parser init with: " + dataSourcePath);
            var input = new StringReader(File.ReadAllText(dataSourcePath));
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            Console.WriteLine("====== Deserializing");
            var root = deserializer.Deserialize<Root>(input);

            Console.WriteLine("====== API info");
            Console.WriteLine("    " + root.Openapi);
            Console.WriteLine("    " + root.Info.Version);
            Console.WriteLine("    " + root.Info.Title);

            Console.WriteLine("====== Paths parsing");
            var generatorMethods = pathsParserService.getParsedPaths(root.Paths);
            Console.WriteLine("====== Components parsing");
            var generatorSchemas = componentsParserService.getParsedComponents(root.Components);

            return new ParserResponse(generatorMethods, generatorSchemas);
        }
    }
}

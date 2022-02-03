using System;
using System.Collections.Generic;
using System.IO;
using OpenSdk.Services.ParserServices;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Path = OpenSdk.ValueObjects.Generator.Path;

namespace OpenSdk.Services
{
    public class ParserService
    {
        private readonly DataSourceService dataSourceService;
        private readonly PathsParserService pathsParserService;
        private readonly ComponentsParserService componentsParserService;

        public ParserService(
            DataSourceService dataSourceService,
            PathsParserService pathsParserService,
            ComponentsParserService componentsParserService
        )
        {
            this.dataSourceService = dataSourceService;
            this.pathsParserService = pathsParserService;
            this.componentsParserService = componentsParserService;
        }

        public void Parse()
        {
            Console.WriteLine("====== Parser init");
            StringReader input = new StringReader(dataSourceService.Get());
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            Console.WriteLine("====== Deserializing");
            Root root = deserializer.Deserialize<Root>(input);

            Console.WriteLine("====== API info");
            Console.WriteLine("    " + root.Openapi);
            Console.WriteLine("    " + root.Info.Version);
            Console.WriteLine("    " + root.Info.Title);

            Console.WriteLine("====== Paths parsing");
            List<Path> generatorMethods = pathsParserService.getParsedPaths(root.Paths);
            Console.WriteLine("====== Components parsing");
            List<Schema> generatorSchemas = componentsParserService.getParsedComponents(root.Components);
        }
    }
}

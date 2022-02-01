using System;
using System.IO;
using OpenSdk.ValueObjects;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace OpenSdk.Services
{
    public class ParserService
    {
        private DataSourceService dataSourceService;

        public ParserService(DataSourceService dataSourceService)
        {
            this.dataSourceService = dataSourceService;
        }

        public void Parse()
        {
            StringReader input = new StringReader(dataSourceService.Get());
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();
            Root root = deserializer.Deserialize<Root>(input);

            Console.WriteLine(root.Openapi);
            Console.WriteLine(root.Info.Version);
            Console.WriteLine(root.Info.Title);

            Console.WriteLine("+++++++++++++");
            foreach (var path in root.Paths)
            {
                Console.WriteLine("+++++++++++++ 1");
                Console.WriteLine(path.Key);
                foreach (var methods in path.Value)
                {
                    Console.WriteLine("+++++++++++++ 2");
                    Console.WriteLine(methods.Key);
                    foreach (var requestBody in methods.Value.requestBody)
                    {
                        Console.WriteLine("+++++++++++++ 3");
                        Console.WriteLine(requestBody.Key);
                        foreach (var content in requestBody.Value)
                        {
                            Console.WriteLine("+++++++++++++ 4");
                            Console.WriteLine(content.Key);
                            foreach (var contentType in content.Value)
                            {
                                Console.WriteLine("+++++++++++++ 5");
                                Console.WriteLine(contentType.Key);
                                foreach (var schema in contentType.Value)
                                {
                                    Console.WriteLine("+++++++++++++ 6");
                                    Console.WriteLine(schema.Key);
                                    Console.WriteLine(schema.Value);
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("------------- 1");
            foreach (var component in root.Components)
            {
                Console.WriteLine(component.Key);
                foreach (var schema in component.Value)
                {
                    Console.WriteLine("------------- 2");
                    Console.WriteLine(schema.Key);
                    Console.WriteLine(schema.Value.Type);

                    foreach (var property in schema.Value.Properties)
                    {
                        Console.WriteLine("------------- 3");
                        Console.WriteLine(property.Key);
                        Console.WriteLine(property.Value.Type);
                    }
                }
            }
        }
    }
}

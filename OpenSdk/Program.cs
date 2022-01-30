using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace OpenApiGenerator
{
    class Program
    {
        private const string Document = @"{
  ""openapi"" : ""3.0.3"",
  ""info"" : {
    ""title"" : ""stackjudge-aws API"",
    ""version"" : ""1.0-SNAPSHOT""
  },
  ""paths"" : {
    ""/ses/send/company-own-email"" : {
      ""post"" : {
        ""tags"" : [ ""Post Send Action"" ],
        ""requestBody"" : {
          ""content"" :  {
            ""multipart/form-data"": {
              ""schema"": {
                 ""$ref"" : ""#/components/schemas/PostCompanyOwnEmailRequest""
               }
            }
          }
        },
        ""responses"" : {
          ""201"" : {
            ""description"" : ""Created""
          }
        }
      }
    }
  },
  ""components"" : {
    ""schemas"": {
      ""PostCompanyOwnEmailRequest"" : {
        ""type"" : ""object"",
        ""properties"" : {
          ""to"" : {
            ""type"" : ""string""
          },
          ""varName"" : {
            ""type"" : ""lofasz""
          },
        }
      }
    }
  }
}";

        static void Main(string[] args)
        {
            StringReader input = new StringReader(Document);
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

    class Root
    {
        public string Openapi { get; set; }
        public Info Info { get; set; }
        public Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> Paths { get; set; }
        public Dictionary<string, Dictionary<string, ComponentsSchemaItem>> Components { get; set; }
    }

    class Info
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }

    class PathUriMethodMethodDetails
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>> requestBody
        {
            get;
            set;
        }
    }

    class ComponentsSchemaItem
    {
        public string Type { get; set; }
        public Dictionary<string, ComponentsSchemaItemProperty> Properties { get; set; }
    }

    class ComponentsSchemaItemProperty
    {
        public string Type { get; set; }
    }
}

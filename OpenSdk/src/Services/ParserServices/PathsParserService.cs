using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public class PathsParserService : IPathsParserService
    {
        public List<Method> getParsedPaths(Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths)
        {
            List<Method> generatorMethods = new List<Method>();

            foreach (var path in paths)
            {
                Console.WriteLine("# path");
                Console.WriteLine(path.Key);
                string pathUri = path.Key;
                foreach (var methods in path.Value)
                {
                    Console.WriteLine("    #method");
                    Console.WriteLine("      " + methods.Key);
                    string pathMethod = methods.Key;
                    foreach (var requestBody in methods.Value.requestBody)
                    {
                        Console.WriteLine("         #request");
                        Console.WriteLine("           " + requestBody.Key);
                        foreach (var content in requestBody.Value)
                        {
                            Console.WriteLine("            #content");
                            Console.WriteLine("              " + content.Key);
                            string pathContentType = content.Key;
                            foreach (var contentType in content.Value)
                            {
                                Console.WriteLine("                #content type");
                                Console.WriteLine("                  " + contentType.Key);
                                foreach (var schema in contentType.Value)
                                {
                                    Console.WriteLine("                    #schema");
                                    Console.WriteLine("                      " + schema.Key);
                                    Console.WriteLine("                      " + schema.Value);
                                    generatorMethods.Add(new Method(
                                        pathUri,
                                        GenerateMethodName(pathUri),
                                        pathMethod,
                                        pathContentType,
                                        schema.Key,
                                        schema.Value,
                                        GetParamObjectName(schema.Key, schema.Value)
                                    ));
                                }
                            }
                        }
                    }
                }
            }

            return generatorMethods;
        }

        private string GetParamObjectName(string type, string value)
        {
            if (type == "$ref")
            {
                return value.Split("/")[3];
            }

            return null;
        }

        private string GenerateMethodName(string path)
        {
            string[] pathParts = path.Split("/");
            List<string> slashCleanPathParts = new List<string>();

            foreach (string pathPart in pathParts)
            {
                string cleanPart = Regex.Replace(pathPart, "[^A-Za-z0-9]", "");
                if (cleanPart.Length > 0)
                {
                    slashCleanPathParts.Add(cleanPart);
                }
            }

            return slashCleanPathParts.Aggregate(
                (x, y) => StringService.UppercaseFirst(x) + StringService.UppercaseFirst(y)
            );
        }
    }
}

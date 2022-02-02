using System;
using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public class PathsParserService
    {
        public List<Path> getParsedPaths(Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths)
        {
            List<Path> generatorMethods = new List<Path>();

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
                                    generatorMethods.Add(new Path(
                                        pathUri,
                                        pathMethod,
                                        pathContentType,
                                        schema.Key,
                                        schema.Value
                                    ));
                                }
                            }
                        }
                    }
                }
            }
            
            return generatorMethods;
        }
    }
}

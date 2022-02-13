using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public class PathsParserService : IPathsParserService
    {
        private readonly ILogger<ParserService> logger;

        public PathsParserService(ILogger<ParserService> logger)
        {
            this.logger = logger;
        }

        public List<Method> getParsedPaths(Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths)
        {
            var generatorMethods = new List<Method>();

            foreach (var path in paths)
            {
                logger.LogInformation("# path");
                logger.LogInformation(path.Key);
                var pathUri = path.Key;
                foreach (var methods in path.Value)
                {
                    logger.LogInformation("    #method");
                    logger.LogInformation("      " + methods.Key);
                    var pathMethod = methods.Key;
                    foreach (var requestBody in methods.Value.requestBody)
                    {
                        logger.LogInformation("         #request");
                        logger.LogInformation("           " + requestBody.Key);
                        foreach (var content in requestBody.Value)
                        {
                            logger.LogInformation("            #content");
                            logger.LogInformation("              " + content.Key);
                            var pathContentType = content.Key;
                            foreach (var contentType in content.Value)
                            {
                                logger.LogInformation("                #content type");
                                logger.LogInformation("                  " + contentType.Key);
                                foreach (var schema in contentType.Value)
                                {
                                    logger.LogInformation("                    #schema");
                                    logger.LogInformation("                      " + schema.Key);
                                    logger.LogInformation("                      " + schema.Value);
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
            var pathParts = path.Split("/");
            var slashCleanPathParts = new List<string>();

            foreach (var pathPart in pathParts)
            {
                var cleanPart = Regex.Replace(pathPart, "[^A-Za-z0-9]", "");
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

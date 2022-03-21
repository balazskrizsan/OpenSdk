using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services.ParserServices
{
    public class PathsParserService : IPathsParserService
    {
        private readonly ILogger<ParserService> logger;

        public PathsParserService(ILogger<ParserService> logger)
        {
            this.logger = logger;
        }

        public List<Method> GetParsedPaths(
            Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths,
            Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components
        )
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

                    var okResponseValueObject = string.Empty;
                    var okResponseDataValueObject = string.Empty;
                    if (methods.Value.Responses.Count > 0)
                    {
                        foreach (var response in methods.Value.Responses)
                        {
                            logger.LogInformation("         #responses: count " + methods.Value.Responses.Count);
                            if (response.Key == "200")
                            {
                                foreach (var contentType in response.Value.Content)
                                {
                                    logger.LogInformation("             #contentType");
                                    logger.LogInformation("               " + contentType.Key);
                                    foreach (var schemas in contentType.Value)
                                    {
                                        foreach (var schema in schemas.Value)
                                        {
                                            logger.LogInformation("             #schema");
                                            logger.LogInformation("               " + schema.Key);
                                            logger.LogInformation("               " + schema.Value);
                                            okResponseValueObject = GetParamObjectName(schema.Key, schema.Value);
                                            okResponseDataValueObject = GetResponseDataValueObject(
                                                schema.Value,
                                                components
                                            );
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (var requestBody in methods.Value.RequestBody)
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
                                        GetParamObjectName(schema.Key, schema.Value),
                                        okResponseValueObject,
                                        okResponseDataValueObject
                                    ));
                                }
                            }
                        }
                    }
                }
            }

            return generatorMethods;
        }

        private string GetResponseDataValueObject(
            string schemaRef,
            Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components
        )
        {
            // @todo check if ref exists
            var dataRef = components
                .Values
                .First()
                .Where(x => x.Key == schemaRef.Split("/")[3])
                .Select(x => x.Value.Properties.Values)
                .First()
                .First()
                .Ref;

            return dataRef.Split("/")[3];
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
                (x, y) => x.UppercaseFirst() + y.UppercaseFirst()
            );
        }
    }
}

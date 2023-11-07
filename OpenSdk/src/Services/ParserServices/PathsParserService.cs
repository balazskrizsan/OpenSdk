using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;
using OpenSdk.ValueObjects.Parser.Parser;

namespace OpenSdk.Services.ParserServices
{
    public class PathsParserService : IPathsParserService
    {
        private readonly ILogger<ParserService> logger;
        private readonly IMapperService mapperService;

        public PathsParserService(
            ILogger<ParserService> logger,
            IMapperService mapperService
        )
        {
            this.logger = logger;
            this.mapperService = mapperService;
        }

        public List<UriMethods> GetParsedPaths(
            Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths,
            Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components
        )
        {
            var generatorMethods = new List<Method>();
            var generatorUriMethods = new List<UriMethods>();

            foreach (var path in paths)
            {
                logger.LogInformation("# path");
                logger.LogInformation(path.Key);
                var pathUri = path.Key;
                Method getMethod = null;
                Method postMethod = null;
                foreach (var methods in path.Value)
                {
                    logger.LogInformation("    #method key: {key}", methods.Key);
                    var pathMethod = methods.Key;

                    var okResponseValueObject = string.Empty;
                    var okResponseDataValueObjectOrType = string.Empty;
                    if (methods.Value.Responses.Count > 0)
                    {
                        foreach (var response in methods.Value.Responses)
                        {
                            logger.LogInformation("         #responses: count {count} ", methods.Value.Responses.Count);
                            if (response.Key == "200")
                            {
                                foreach (var contentType in response.Value.Content)
                                {
                                    logger.LogInformation("             #contentType {Key}", contentType.Key);
                                    foreach (var schemas in contentType.Value)
                                    {
                                        foreach (var schema in schemas.Value)
                                        {
                                            logger.LogInformation("             #schema key: {key} value: {value}", schema.Key, schema.Value);
                                            okResponseValueObject = GetParamObjectName(schema.Key, schema.Value);
                                            okResponseDataValueObjectOrType = GetResponseDataValueObjectOrPrimitive(
                                                schema.Value,
                                                components
                                            );
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (methods.Key == "get")
                    {
                        String paramClassName = $"Get{CreateClassNameFromPath(pathUri)}Params";
                        String paramVarName = $"get{CreateClassNameFromPath(pathUri)}Params";

                        getMethod = new Method(
                            pathUri,
                            CreateClassNameFromPath(pathUri),
                            pathMethod,
                            null,
                            "",
                            "",
                            paramVarName,
                            okResponseValueObject,
                            okResponseDataValueObjectOrType,
                            new CustomSchema(paramClassName, GenerateParameters(methods.Value.Parameters))
                        );
                        generatorMethods.Add(getMethod);
                    }

                    if (methods.Key == "post")
                    {
                        foreach (var requestBody in methods.Value.RequestBody)
                        {
                            logger.LogInformation("         #request requestBody.Key: {key}", requestBody.Key);
                            foreach (var content in requestBody.Value)
                            {
                                logger.LogInformation("            #content content.Key: {key}", content.Key);
                                var pathContentType = content.Key;
                                foreach (var schemaItem in content.Value)
                                {
                                    logger.LogInformation("                #content type contentType.Key: {key}", schemaItem.Key);

                                    var reference = schemaItem.Value.Ref;
                                    if (reference is not null)
                                    {
                                        logger.LogInformation("                    #schema - reference schema: {reference}", reference);
                                        postMethod = new Method(
                                            pathUri,
                                            CreateClassNameFromPath(pathUri),
                                            pathMethod,
                                            pathContentType,
                                            "$ref",
                                            reference,
                                            GetParamObjectName("$ref", reference),
                                            okResponseValueObject,
                                            okResponseDataValueObjectOrType
                                        );
                                        generatorMethods.Add(postMethod);
                                        
                                        continue;
                                    }

                                    var customName = "Post" + CreateClassNameFromPath(pathUri);
                                    logger.LogInformation("                    #schema  - custom schema: {customName}", customName);
                                    postMethod = new Method(
                                        pathUri,
                                        CreateClassNameFromPath(pathUri),
                                        pathMethod,
                                        pathContentType,
                                        "custom",
                                        customName,
                                        customName,
                                        okResponseValueObject,
                                        okResponseDataValueObjectOrType,
                                        new CustomSchema(customName, GenerateParameters(methods.Value.Parameters))
                                    );
                                    generatorMethods.Add(postMethod);
                                }
                            }
                        }
                    }
                }

                generatorUriMethods.Add(new UriMethods(pathUri, CreateClassNameFromPath(pathUri), getMethod, postMethod));
            }

            return generatorUriMethods;
        }

        private Dictionary<string, Property> GenerateParameters(List<Parameter> rawParameters)
        {
            var parameters = new Dictionary<string, Property>();
            foreach (var parameter in rawParameters)
            {
                logger.LogInformation("         #parameters name: {name} schema.ref: {ref} schema.type {type}",
                    parameter.Name,
                    parameter.Schema.Ref,
                    parameter.Schema.Type
                );

                parameters.Add(parameter.Name, new Property(
                    "",
                    parameter.Schema.Type,
                    "",
                    parameter.Schema.Type == "array" ? parameter.Schema.Items.First().Value : null
                ));
            }
            
            return parameters;
        }

        private string GetResponseDataValueObjectOrPrimitive(
            string schemaRef,
            Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components
        )
        {
            var schemaName = schemaRef.Split("/")[3];

            var componentsSchemaItemProperty = components
                .Values
                .First()
                .Where(x => x.Key == schemaName)
                .Select(x => x.Value.Properties.Values)
                .First()
                .First();

            string generic = null;
            if (componentsSchemaItemProperty.Type == "array")
            {
                generic = componentsSchemaItemProperty.Items.First().Value.Split("/")[3];
            }

            return null != componentsSchemaItemProperty.Ref
                ? componentsSchemaItemProperty.Ref.Split("/")[3]
                : mapperService.TypeMapper(componentsSchemaItemProperty.Type, generic);
        }

        private string GetParamObjectName(string type, string value)
        {
            if (type == "$ref")
            {
                return value.Split("/")[3];
            }

            return null;
        }

        private string CreateClassNameFromPath(string path)
        {
            path = path.Replace("-", "/");
            path = Regex.Replace(path, @"{([A-Za-z0-9])([A-Za-z0-9]*)}", "By___($1)___$2");

            var pathVarFirstLetters = Regex.Matches(path, @"___\([A-Za-z0-9]\)___");
            if (pathVarFirstLetters.Count > 0)
            {
                foreach (Match pathVarFirstLetterWithWrap in pathVarFirstLetters)
                {
                    var pathVarFirstLetter = pathVarFirstLetterWithWrap.Value
                        .Replace("___(", "")
                        .Replace(")___", "")
                        .ToUpper();
                    var x = pathVarFirstLetterWithWrap.Value;
                    path = path.Replace(pathVarFirstLetterWithWrap.Value, pathVarFirstLetter);
                }
            }

            var slashCleanPathParts = new List<string>();
            foreach (var pathPart in path.Split("/"))
            {
                var cleanPart = Regex.Replace(pathPart, "[^A-Za-z0-9]", "");
                if (cleanPart.Length > 0)
                {
                    slashCleanPathParts.Add(cleanPart);
                }
            }

            return StringService.UppercaseFirst(
                slashCleanPathParts.Aggregate(
                    (x, y) => StringService.UppercaseFirst(x) + StringService.UppercaseFirst(y)
                )
            );
        }
    }
}
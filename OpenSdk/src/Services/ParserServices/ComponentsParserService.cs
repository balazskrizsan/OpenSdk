using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OpenSdk.Services.Filters;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services.ParserServices;

public class ComponentsParserService : IComponentsParserService
{
    private readonly ILogger<ParserService> logger;
    private readonly IValueObjectByMethodTypeFilterService valueObjectByMethodTypeFilterService;

    public ComponentsParserService(ILogger<ParserService> logger, IValueObjectByMethodTypeFilterService valueObjectByMethodTypeFilterService)
    {
        this.logger = logger;
        this.valueObjectByMethodTypeFilterService = valueObjectByMethodTypeFilterService;
    }

    public List<Schema> GetParsedComponents(
        Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components,
        Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths
    )
    {
        var generatorSchemas = new List<Schema>();

        var valueObjectByMethodType = valueObjectByMethodTypeFilterService.Get(paths);

        foreach (var (componentType, componentTypeItems) in components)
        {
            // if componentType == schemas
            logger.LogInformation("#component");
            logger.LogInformation(componentType);
            foreach (var (schemaName, schema) in componentTypeItems)
            {
                logger.LogInformation("    #schema");
                logger.LogInformation("      " + schemaName);
                logger.LogInformation("      " + schema.Type);
                Dictionary<string, string> schemaParams = new Dictionary<string, string>();
                var properties = schema?.Properties;

                if (null == properties)
                {
                    continue;
                }

                foreach (var property in properties)
                {
                    logger.LogInformation("        #properties");
                    logger.LogInformation("          " + property.Key);
                    logger.LogInformation("          " + property.Value.Ref);
                    logger.LogInformation("          " + property.Value.Type);

                    if (property.Value.Ref == null)
                    {
                        schemaParams.Add(property.Key, property.Value.Type);
                    }
                    else
                    {
                        schemaParams.Add(property.Value.Ref, property.Value.Ref);
                    }
                }

                List<string> relatedMethodTypes;
                valueObjectByMethodType.TryGetValue($"#/components/schemas/{schemaName}", out relatedMethodTypes);

                var hasPost = false;
                var hasGet = false;
                if (null != relatedMethodTypes)
                {
                    if (relatedMethodTypes.Contains("post"))
                    {
                        hasPost = true;
                    }

                    if (relatedMethodTypes.Contains("get"))
                    {
                        hasGet = true;
                    }
                }

                generatorSchemas.Add(new Schema(schemaName, schema.Type, schemaParams, hasGet, hasPost));
            }
        }

        return generatorSchemas;
    }
}
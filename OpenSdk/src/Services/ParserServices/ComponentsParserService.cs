using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenSdk.Constants;
using OpenSdk.Services.Filters;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;
using OpenSdk.ValueObjects.Parser.Parser;

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
            logger.LogInformation("#component componentType: {componentType}", componentType);
            foreach (var (schemaName, schema) in componentTypeItems)
            {
                logger.LogInformation("    #schema schemaName: {schemaName} schema.Type: {type}", schemaName, schema.Type);
                Dictionary<string, Property> schemaParams = new Dictionary<string, Property>();
                var properties = schema?.Properties;

                if (null == properties)
                {
                    continue;
                }

                foreach (var property in properties)
                {
                    logger.LogInformation("        #properties key: {key} $ref {ref} type: {type}", property.Key, property.Value.Ref, property.Value.Type);

                    if (property.Value.Ref == null)
                    {
                        string parameterGeneric = null;
                        if (property.Value.Type == "array")
                        {
                            try
                            {
                                parameterGeneric = property.Value.Items.First().Value.Split("/")[3];
                            }
                            catch (Exception)
                            {
                                parameterGeneric = property.Value.Items.First().Value;
                            }
                        }

                        schemaParams.Add(property.Key, new Property(property.Key, property.Value.Type, PropertyValueType.TYPE, parameterGeneric));
                    }
                    else
                    {
                        schemaParams.Add(property.Key, new Property(property.Key, property.Value.Ref, PropertyValueType.REF));
                    }
                }

                var relatedMethods = valueObjectByMethodType.Where(v => v.Key == schemaName).SelectMany(i => i.Value).ToList();

                var hasPost = false;
                var hasGet = false;
                if (relatedMethods.Any())
                {
                    if (relatedMethods.Contains("post"))
                    {
                        hasPost = true;
                    }

                    if (relatedMethods.Contains("get"))
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
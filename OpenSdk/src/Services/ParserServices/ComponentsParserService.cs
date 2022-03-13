using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public class ComponentsParserService : IComponentsParserService
    {
        private readonly ILogger<ParserService> logger;

        public ComponentsParserService(ILogger<ParserService> logger)
        {
            this.logger = logger;
        }

        public List<Schema> GetParsedComponents(Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components)
        {
            var generatorSchemas = new List<Schema>();

            foreach (var component in components)
            {
                logger.LogInformation("#component");
                logger.LogInformation(component.Key);
                foreach (var schema in component.Value)
                {
                    logger.LogInformation("    #schema");
                    logger.LogInformation("      " + schema.Key);
                    logger.LogInformation("      " + schema.Value.Type);
                    Dictionary<string, string> schemaParams = new Dictionary<string, string>();
                    var properties = schema.Value?.Properties;

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

                    generatorSchemas.Add(new Schema(schema.Key, schema.Value.Type, schemaParams));
                }
            }

            return generatorSchemas;
        }
    }
}

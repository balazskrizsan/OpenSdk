using System;
using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public class ComponentsParserService : IComponentsParserService
    {
        public List<Schema> getParsedComponents(Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components)
        {
            List<Schema> generatorSchemas = new List<Schema>();

            foreach (var component in components)
            {
                Console.WriteLine("#component");
                Console.WriteLine(component.Key);
                foreach (var schema in component.Value)
                {
                    Console.WriteLine("    #schema");
                    Console.WriteLine("      " + schema.Key);
                    Console.WriteLine("      " + schema.Value.Type);
                    Dictionary<string, string> schemaParams = new Dictionary<string, string>();
                    foreach (var property in schema.Value.Properties)
                    {
                        Console.WriteLine("        #properties");
                        Console.WriteLine("          " + property.Key);
                        Console.WriteLine("          " + property.Value.Type);
                        schemaParams.Add(property.Key, property.Value.Type);
                    }

                    generatorSchemas.Add(new Schema(schema.Key, schema.Value.Type, schemaParams));
                }
            }
            
            return generatorSchemas;
        }
    }
}

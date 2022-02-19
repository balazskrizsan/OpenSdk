using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public interface IComponentsParserService
    {
        List<Schema> getParsedComponents(Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components);
    }
}
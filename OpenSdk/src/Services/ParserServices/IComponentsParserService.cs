using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services.ParserServices
{
    public interface IComponentsParserService
    {
        List<Schema> GetParsedComponents(
            Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components,
            Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths
        );
    }
}

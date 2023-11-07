using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;
using OpenSdk.ValueObjects.Parser.Parser;

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

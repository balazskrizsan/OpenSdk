using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services.ParserServices
{
    public interface IPathsParserService
    {
        List<UriMethods> GetParsedPaths(
            Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths,
            Dictionary<string, Dictionary<string, ComponentsSchemaItem>> components
        );
    }
}

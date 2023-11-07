using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;
using OpenSdk.ValueObjects.Parser.Parser;

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

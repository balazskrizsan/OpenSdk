using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public interface IPathsParserService
    {
        List<Path> getParsedPaths(Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths);
    }
}

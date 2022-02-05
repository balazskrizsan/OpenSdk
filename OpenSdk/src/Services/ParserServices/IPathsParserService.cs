using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.ParserServices
{
    public interface IPathsParserService
    {
        List<Method> getParsedPaths(Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> paths);
    }
}

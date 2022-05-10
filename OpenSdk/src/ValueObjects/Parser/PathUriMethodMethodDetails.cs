using System.Collections.Generic;

namespace OpenSdk.ValueObjects.Parser
{
    public class PathUriMethodMethodDetails
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string>>>>
            RequestBody { get; set; }

        public Dictionary<string, PathUriMethodMethodDetailsResponses> Responses { get; set; }

        public List<Parameter> Parameters { get; set; }
    }

    public class PathUriMethodMethodDetailsResponses
    {
        public string Description { get; set; }

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Content { get; set; }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string In { get; set; }
        public bool Required { get; set; }
        public Dictionary<string, string> Schema { get; set; }
    }
}
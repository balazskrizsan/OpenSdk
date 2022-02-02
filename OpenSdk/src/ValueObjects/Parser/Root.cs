using System.Collections.Generic;

namespace OpenSdk.ValueObjects
{
    public class Root
    {
        public string Openapi { get; set; }
        public Info Info { get; set; }
        public Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> Paths { get; set; }
        public Dictionary<string, Dictionary<string, ComponentsSchemaItem>> Components { get; set; }
    }
}

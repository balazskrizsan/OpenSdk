using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace OpenSdk.ValueObjects.Parser.Parser
{
    public class PathUriMethodMethodDetails
    {
        public Dictionary< // methods.Value
                string, // content
                Dictionary<
                    string, // multipart/form-data
                    Dictionary<
                        string, // schema
                        SchemaItems>>>
            RequestBody { get; set; }

        public Dictionary<string, PathUriMethodMethodDetailsResponses> Responses { get; set; }

        public List<Parameter> Parameters { get; set; }
    }

    public class PathUriMethodMethodDetailsResponses
    {
        public string Description { get; set; }

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Content { get; set; }
    }

    public class SchemaItems
    {
        public string Type { get; set; }
        public Dictionary<string, SchemaItemsPropertyItem> Properties { get; set; }
        [YamlMember(Alias = "$ref")] public string Ref { get; set; }
    }

    public class SchemaItemsPropertyItem
    {
        public string Type { get; set; }
        public string Format { get; set; }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string In { get; set; }
        public bool Required { get; set; }
        public ComponentsSchemaItemProperty Schema { get; set; }
    }
}
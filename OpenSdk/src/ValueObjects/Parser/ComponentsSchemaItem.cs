using System.Collections.Generic;

namespace OpenSdk.ValueObjects
{
    public class ComponentsSchemaItem
    {
        public string Type { get; set; }
        public Dictionary<string, ComponentsSchemaItemProperty> Properties { get; set; }
    }
}

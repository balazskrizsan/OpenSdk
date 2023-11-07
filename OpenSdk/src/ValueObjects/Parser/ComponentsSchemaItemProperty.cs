using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace OpenSdk.ValueObjects.Parser;

public class ComponentsSchemaItemProperty
{
    public string Type { get; set; }

    [YamlMember(Alias = "$ref")] public string Ref { get; set; }

    public Dictionary<string, string> Items { get; set; }
}
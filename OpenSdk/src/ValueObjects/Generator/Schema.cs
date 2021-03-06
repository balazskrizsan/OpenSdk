using System.Collections.Generic;

namespace OpenSdk.ValueObjects.Generator;

public class Schema
{
    public string Name { get; }
    public string Type { get; }
    public Dictionary<string, string> Parameters { get; }
    public bool HasGet { get; }
    public bool HasPost { get; }

    public Schema(string name, string type, Dictionary<string, string> parameters, bool hasGet, bool hasPost)
    {
        Name = name;
        Type = type;
        Parameters = parameters;
        HasGet = hasGet;
        HasPost = hasPost;
    }
}
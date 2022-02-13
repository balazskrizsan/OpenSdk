using System.Collections.Generic;
using System.ComponentModel;

namespace OpenSdk.ValueObjects.Generator
{
    public class Schema
    {
        [ReadOnly(true)] public string Name { get; }
        [ReadOnly(true)] public string Type { get; }
        [ReadOnly(true)] public Dictionary<string, string> Parameters { get; }

        public Schema(string name, string type, Dictionary<string, string> parameters)
        {
            Name = name;
            Type = type;
            Parameters = parameters;
        }
    }
}

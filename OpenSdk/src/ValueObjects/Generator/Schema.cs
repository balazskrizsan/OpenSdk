using System.Collections.Generic;
using System.ComponentModel;

namespace OpenSdk.ValueObjects.Generator
{
    public class Schema
    {
        public string Name { get; }
        public string Type { get; }
        public Dictionary<string, string> Parameters { get; }

        public Schema(string name, string type, Dictionary<string, string> parameters)
        {
            Name = name;
            Type = type;
            Parameters = parameters;
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.ValueObjects
{
    public class ParserResponse
    {
        public List<Method> Methods { get; }
        public List<Schema> Schemas { get; }
        public ParserResponse(List<Method> Methods, List<Schema> Schemas)
        {
            this.Methods = Methods;
            this.Schemas = Schemas;
        }
    }
}

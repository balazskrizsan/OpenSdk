using System.Collections.Generic;
using System.ComponentModel;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.ValueObjects
{
    public class ParserResponse
    {
        public List<UriMethods> UriMethods { get; }
        public List<Schema> Schemas { get; }
        public ParserResponse(List<UriMethods> uriMethods, List<Schema> schemas)
        {
            this.UriMethods = uriMethods;
            this.Schemas = schemas;
        }
    }
}

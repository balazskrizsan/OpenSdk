using System.ComponentModel;

namespace OpenSdk.ValueObjects.Generator
{
    public class Path
    {
        [ReadOnly(true)] public string Uri { get; }
        [ReadOnly(true)] public string Method { get; }
        [ReadOnly(true)] public string ContentType { get; }
        [ReadOnly(true)] public string ParamSchemaType { get; }
        [ReadOnly(true)] public string ParamSchemaValue { get; }

        public Path(string uri, string method, string contentType, string paramSchemaType, string paramSchemaValue)
        {
            Uri = uri;
            Method = method;
            ContentType = contentType;
            ParamSchemaType = paramSchemaType;
            ParamSchemaValue = paramSchemaValue;
        }
    }
}

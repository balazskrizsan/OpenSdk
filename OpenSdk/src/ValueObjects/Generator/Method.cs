using System.ComponentModel;

namespace OpenSdk.ValueObjects.Generator
{
    public class Method
    {
        [ReadOnly(true)] public string Uri { get; }
        [ReadOnly(true)] public string MethodName { get; }
        [ReadOnly(true)] public string MethodType { get; }
        [ReadOnly(true)] public string ContentType { get; }
        [ReadOnly(true)] public string ParamSchemaType { get; }
        [ReadOnly(true)] public string ParamSchemaValue { get; }
        [ReadOnly(true)] public string ParamObjectName { get; }

        public Method(
            string uri,
            string methodName,
            string methodType,
            string contentType,
            string paramSchemaType,
            string paramSchemaValue,
            string paramObjectName
        )
        {
            Uri = uri;
            MethodName = methodName;
            MethodType = methodType;
            ContentType = contentType;
            ParamSchemaType = paramSchemaType;
            ParamSchemaValue = paramSchemaValue;
            ParamObjectName = paramObjectName;
        }
    }
}
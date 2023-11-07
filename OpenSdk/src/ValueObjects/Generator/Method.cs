namespace OpenSdk.ValueObjects.Parser.Generator
{
    public class Method
    {
        public string Uri { get; } // will be removed
        public string MethodName { get; } // will be removed
        public string MethodType { get; } // will be removed
        public string ContentType { get; }
        public string ParamSchemaType { get; }
        public string ParamSchemaValue { get; }
        public string ParamObjectName { get; }
        public string OkResponseValueObject { get; }
        public string OkResponseDataValueObjectOrType { get; }
        public CustomSchema CustomSchema { get; }

        public Method(
            string uri,
            string methodName,
            string methodType,
            string contentType,
            string paramSchemaType,
            string paramSchemaValue,
            string paramObjectName,
            string okResponseValueObject,
            string okResponseDataValueObjectOrType,
            CustomSchema customSchema = null
        )
        {
            Uri = uri;
            MethodName = methodName;
            MethodType = methodType;
            ContentType = contentType;
            ParamSchemaType = paramSchemaType;
            ParamSchemaValue = paramSchemaValue;
            ParamObjectName = paramObjectName;
            OkResponseValueObject = okResponseValueObject;
            OkResponseDataValueObjectOrType = okResponseDataValueObjectOrType;
            CustomSchema = customSchema;
        }
    }
}

using System.Collections.Generic;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.UnitTest.TestHelpers.FakeBuilders;

public class MethodFakeBuilder
{
    private string Uri { get; set; } = "/s3/upload";
    private string MethodName { get; set; } = "S3Upload";
    private string MethodType { get; set; } = "post";
    private string ContentType { get; set; } = "multipart/form-data";
    private string ParamSchemaType { get; set; } = "$ref";
    private string ParamSchemaValue { get; set; } = "#/components/schemas/PostUploadRequest";
    private string ParamObjectName { get; set; } = "CdnServicePutResponse";
    private string OkResponseValueObject { get; set; } = "ApiResponseDataCdnServicePutResponse";
    private string OkResponseDataValueObject { get; set; } = "CdnServicePutResponse";

    public Method Build()
    {
        return new Method(
            Uri,
            MethodName,
            MethodType,
            ContentType,
            ParamSchemaType,
            ParamSchemaValue,
            ParamObjectName,
            OkResponseValueObject,
            OkResponseDataValueObject
        );
    }

    public List<Method> GetAsList()
    {
        new MethodFakeBuilder().Uri = "";
        return new List<Method> { Build() };
    }
}
using System.Collections.Generic;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.UnitTest.TestHelpers.FakeBuilders;

public class SchemaFakeBuilder
{
    public string Name { get; } = "ApiResponseDataCdnServicePutResponse";
    public string Name2 { get; } = "CdnServicePutResponse";
    public string Type { get; } = "object";
    public string Type2 { get; } = "object";
    public bool hasGet { get; } = true;
    public bool hasPost { get; } = true;
    public bool hasGet2 { get; } = true;
    public bool hasPost2 { get; } = true;

    public Dictionary<string, string> Parameters { get; } = new()
    {
        { "#/components/schemas/CdnServicePutResponse", "#/components/schemas/CdnServicePutResponse" },
        { "success", "boolean" },
        { "errorCode", "integer" },
        { "requestId", "string" }
    };

    public Dictionary<string, string> Parameters2 { get; } = new()
    {
        { "path", "string" },
        { "fileName", "string" },
        { "s3eTag", "string" },
        { "s3contentMd5", "string" }
    };

    public Schema Build()
    {
        return new Schema(Name, Type, Parameters, hasGet, hasPost);
    }

    public List<Schema> GetAsList()
    {
        return new List<Schema> { Build() };
    }

    public List<Schema> BuildBothAsList()
    {
        return new()
        {
            new Schema(Name, Type, Parameters, hasGet, hasPost),
            new Schema(Name2, Type2, Parameters2, hasGet, hasPost)
        };
    }
}
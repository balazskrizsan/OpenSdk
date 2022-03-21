using System;
using Microsoft.Extensions.Logging;
using OpenSdk.Registries;

namespace OpenSdk.Services.GeneratorServices;

public class MapperService : IMapperService
{
    private readonly ILogger<ParserService> logger;
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;

    public MapperService(
        ILogger<ParserService> logger,
        IApplicationArgumentRegistry applicationArgumentRegistry
    )
    {
        this.logger = logger;
        this.applicationArgumentRegistry = applicationArgumentRegistry;
    }

    public string TypeMapper(string openApiType)
    {
        switch (openApiType)
        {
            case "string":
                return "String";
            case "boolean":
                return "Boolean";
            case "integer":
                return "Integer";
            case "#/components/schemas/FileUpload":
                return "HttpEntity<ByteArrayResource>";
            default:
                try
                {
                    return openApiType.Split("/")[3];
                }
                catch (Exception)
                {
                    throw new Exception("No type found for: " + openApiType);
                }
        }
    }

    public string VarNameMapper(string varName)
    {
        switch (varName)
        {
            case "#/components/schemas/FileUpload":
                return "content";
            default:
                try
                {
                    return varName.Split("/")[3].LowercaseFirst();
                }
                catch (Exception)
                {
                    return varName;
                }
        }
    }
}
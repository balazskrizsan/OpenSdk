using System;
using Microsoft.Extensions.Logging;
using OpenSdk.Registries;

namespace OpenSdk.Services.GeneratorServices;

public class MapperService : IMapperService
{
    private readonly ILogger<MapperService> logger;
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;

    public MapperService(
        ILogger<MapperService> logger,
        IApplicationArgumentRegistry applicationArgumentRegistry
    )
    {
        this.logger = logger;
        this.applicationArgumentRegistry = applicationArgumentRegistry;
    }

    public string TypeMapper(string openapiType)
    {
        var language = applicationArgumentRegistry.OutputLanguage;

        switch (openapiType)
        {
            case "string":
                return GetLanguageSpecificType(language, "string");
            case "boolean":
                return GetLanguageSpecificType(language, "boolean");
            case "integer":
                return GetLanguageSpecificType(language, "integer");
            case "#/components/schemas/FileUpload":
                return "HttpEntity<ByteArrayResource>";
            default:
                try
                {
                    return openapiType.Split("/")[3];
                }
                catch (Exception)
                {
                    throw new Exception("No type found for: " + openapiType);
                }
        }
    }

    private string GetLanguageSpecificType(string language, string type)
    {
        if (language == "Java" && type == "string")
        {
            return "String";
        }

        if (language == "Java" && type == "boolean")
        {
            return "Boolean";
        }

        if (language == "Java" && type == "boolean")
        {
            return "Boolean";
        }

        if (language == "TypeScript" && type == "string")
        {
            return "string";
        }

        if (language == "TypeScript" && type == "boolean")
        {
            return "boolean";
        }

        if (language == "TypeScript" && type == "integer")
        {
            return "number";
        }

        return type;
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
                    return StringService.LowercaseFirst(varName.Split("/")[3]);
                }
                catch (Exception)
                {
                    return varName;
                }
        }
    }
}
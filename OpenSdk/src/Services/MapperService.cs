using System;
using Microsoft.Extensions.Logging;
using OpenSdk.Constants;
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
            case OpenApiVariableConsts.STRING:
                return GetLanguageSpecificType(language, OpenApiVariableConsts.STRING);
            case OpenApiVariableConsts.BOOL:
                return GetLanguageSpecificType(language, OpenApiVariableConsts.BOOL);
            case OpenApiVariableConsts.INT:
                return GetLanguageSpecificType(language, OpenApiVariableConsts.INT);
            case OpenApiVariableConsts.ARRAY:
                return GetLanguageSpecificType(language, OpenApiVariableConsts.ARRAY, OpenApiVariableConsts.STRING);
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

    private string GetLanguageSpecificType(string language, string type, string generic = null)
    {
        if (LanguagesConsts.JAVA == language)
        {
            switch (type)
            {
                case OpenApiVariableConsts.STRING: return JavaVariableConsts.STRING;
                case OpenApiVariableConsts.BOOL: return JavaVariableConsts.BOOL;
                case OpenApiVariableConsts.INT: return JavaVariableConsts.INT;
                case OpenApiVariableConsts.ARRAY: return JavaVariableConsts.LIST + TryGetGeneric(LanguagesConsts.JAVA, generic);
            }
        }

        if (LanguagesConsts.TYPE_SCRIPT == language)
        {
            switch (type)
            {
                case OpenApiVariableConsts.STRING: return TypeScriptVariableConsts.STRING;
                case OpenApiVariableConsts.BOOL: return TypeScriptVariableConsts.BOOL;
                case OpenApiVariableConsts.INT: return TypeScriptVariableConsts.NUMBER;
            }
        }

        return type;
    }

    private string TryGetGeneric(string lang, string generic)
    {
        if (lang == LanguagesConsts.JAVA)
        {
            switch (generic)
            {
                case OpenApiVariableConsts.STRING: return $"<{JavaVariableConsts.STRING}>";
            }
        }

        throw new Exception($"lang+generic not found {lang} {generic}");
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
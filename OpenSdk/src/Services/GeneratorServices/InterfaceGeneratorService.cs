using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OpenSdk.Constants;
using OpenSdk.Registries;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;

namespace OpenSdk.Services;

public class InterfaceGeneratorService : IInterfaceGeneratorService
{
    private readonly IMapperService mapperService;
    private readonly ITemplateService templateService;
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;
    private readonly ILogger<InterfaceGeneratorService> logger;

    public InterfaceGeneratorService(
        IMapperService mapperService,
        ITemplateService templateService,
        IApplicationArgumentRegistry applicationArgumentRegistry,
        ILogger<InterfaceGeneratorService> logger
    )
    {
        this.mapperService = mapperService;
        this.templateService = templateService;
        this.applicationArgumentRegistry = applicationArgumentRegistry;
        this.logger = logger;
    }

    public List<File> GetGenerateFiles(List<UriMethods> uriMethods)
    {
        var interfaceTemplatePath = GetInterfaceTemplate();
        var namespaceValue = applicationArgumentRegistry.NamespacePrefix + ".schema_interfaces";
        var destinationFolder = "\\" + namespaceValue.Replace(".", "\\");

        var files = new List<File>();
        foreach (var uriMethod in uriMethods)
        {
            var interfaceName = "I" + uriMethod.PathClassName;

            var hasGetMethod = uriMethod.GetMethod != null;
            var hasPostMethod = uriMethod.PostMethod != null;

            var context = new
            {
                InterfaceName = interfaceName,
                Namespace = namespaceValue,
                MethodUri = uriMethod.Uri,
                NamespacePrefix = applicationArgumentRegistry.NamespacePrefix,
                // Get
                HasGetMethod = hasGetMethod,
                GetParamObjectVarName = hasGetMethod
                    ? StringService.LowercaseFirst(uriMethod.GetMethod.ParamObjectName)
                    : null,
                GetReturnType = GetGetReturnType(hasGetMethod, uriMethod.GetMethod),
                // Post
                HasPostMethod = hasPostMethod,
                PostParamObjectVarName = hasPostMethod
                    ? StringService.LowercaseFirst(uriMethod.PostMethod.ParamObjectName)
                    : null,
                PostReturnType = hasPostMethod
                    ? !string.IsNullOrWhiteSpace(uriMethod.PostMethod.OkResponseValueObject)
                        ? "StdResponse<" + uriMethod.PostMethod.OkResponseDataValueObjectOrType + ">"
                        : "void"
                    : null,
                PostAsyncReturnType = hasPostMethod
                    ? !string.IsNullOrWhiteSpace(uriMethod.PostMethod.OkResponseValueObject)
                        ? "Future<StdResponse<" + uriMethod.PostMethod.OkResponseDataValueObjectOrType + ">>"
                        : "void"
                    : null
            };
            var fileName = interfaceName + GetFileExtension();

            var generatedInterface = templateService.RenderTemplate(interfaceTemplatePath, context);
            files.Add(new File(destinationFolder, fileName, interfaceName, generatedInterface, GeneratedFileTypeConsts.INTERFACE));
            logger.LogInformation("    - {destinationFolder}\\{fileName} ", destinationFolder, fileName);
        }

        return files;
    }

    private string GetGetReturnType(bool hasGetMethod, Method getMethod)
    {
        if (!hasGetMethod)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(getMethod.OkResponseValueObject))
        {
            return "void";
        }

        var language = applicationArgumentRegistry.OutputLanguage;

        switch (applicationArgumentRegistry.OutputLanguage)
        {
            case "TypeScript":
                var c = mapperService.IsPrimitive(getMethod.OkResponseDataValueObjectOrType)? "" : "valueObject.";

                return "Observable<StdResponse<" + c + getMethod.OkResponseDataValueObjectOrType + ">>";
            case "Java": return "StdResponse<" + getMethod.OkResponseDataValueObjectOrType + ">";
            default: throw new Exception("Language is not supported: " + language);
        }
    }

    private string GetFileExtension()
    {
        var language = applicationArgumentRegistry.OutputLanguage;

        switch (language)
        {
            case "TypeScript": return ".ts";
            case "Java": return ".java";
            default: throw new Exception("Language is not supported: " + language);
        }
    }

    private string GetInterfaceTemplate()
    {
        var language = applicationArgumentRegistry.OutputLanguage;

        switch (language)
        {
            case "TypeScript": return @"./templates/Interface.TypeScript.liquid";
            case "Java": return @"./templates/Interface.Java.liquid";
            default: throw new Exception("Language interface template missing: " + language);
        }
    }

    private string GetExecuteParameterType(string methodType)
    {
        switch (methodType)
        {
            case "post": return "IOpenSdkPostable";
            case "get": return "IOpenSdkGetable";
            default: throw new Exception("Method interface is not yet implemented: " + methodType);
        }
    }
}
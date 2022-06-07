using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OpenSdk.Registries;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices;

public class InterfaceGeneratorService : IInterfaceGeneratorService
{
    private readonly ITemplateService templateService;
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;
    private readonly ILogger<InterfaceGeneratorService> logger;

    public InterfaceGeneratorService(
        ITemplateService templateService,
        IApplicationArgumentRegistry applicationArgumentRegistry,
        ILogger<InterfaceGeneratorService> logger
    )
    {
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
                        ? "StdResponse<" + uriMethod.PostMethod.OkResponseDataValueObject + ">"
                        : "void"
                    : null,
                PostAsyncReturnType = hasPostMethod
                    ? !string.IsNullOrWhiteSpace(uriMethod.PostMethod.OkResponseValueObject)
                        ? "Future<StdResponse<" + uriMethod.PostMethod.OkResponseDataValueObject + ">>"
                        : "void"
                    : null
            };
            var fileName = interfaceName + GetFileExtension();

            var generatedInterface = templateService.RenderTemplate(interfaceTemplatePath, context);
            files.Add(new File(destinationFolder, fileName, generatedInterface));
            logger.LogInformation("    - {destinationFolder}/{fileName} ", destinationFolder, fileName);
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
            case "TypeScript": return "Observable<StdResponse<" + getMethod.OkResponseDataValueObject + ">>";
            case "Java": return "StdResponse<" + getMethod.OkResponseDataValueObject + ">";
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
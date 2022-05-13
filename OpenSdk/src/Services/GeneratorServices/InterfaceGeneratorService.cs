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

    public List<File> GetGenerateFiles(List<Method> methods)
    {
        var interfaceTemplatePath = GetInterfaceTemplate();
        var namespaceValue = applicationArgumentRegistry.NamespacePrefix + ".schema_interfaces";
        var destinationFolder = "\\" + namespaceValue.Replace(".", "\\");

        var files = new List<File>();
        foreach (var method in methods)
        {
            var executeParameterType = GetExecuteParameterType(method.MethodType);

            var interfaceName = "I" + method.MethodName;

            var context = new
            {
                InterfaceName = interfaceName,
                Namespace = namespaceValue,
                ParamObjectClassName = method.ParamObjectName,
                ParamObjectVarName = StringService.LowercaseFirst(method.ParamObjectName),
                MethodUri = method.Uri,
                MethodType = method.MethodType,
                ExecReturnType = "void",
                NamespacePrefix = applicationArgumentRegistry.NamespacePrefix,
                ExecuteParameterType = executeParameterType
            };
            var fileName = interfaceName + GetFileExtension();

            var generatedInterface = templateService.RenderTemplate(interfaceTemplatePath, context);
            files.Add(new File(destinationFolder, fileName, generatedInterface));
            logger.LogInformation("    - {destinationFolder}/{fileName} ", destinationFolder, fileName);

            if (!string.IsNullOrWhiteSpace(method.OkResponseValueObject))
            {
                var interfaceNameWithReturn = interfaceName + "WithReturn";

                context = new
                {
                    InterfaceName = interfaceNameWithReturn,
                    Namespace = namespaceValue,
                    ParamObjectClassName = method.ParamObjectName,
                    ParamObjectVarName = StringService.LowercaseFirst(method.ParamObjectName),
                    MethodUri = method.Uri,
                    MethodType = method.MethodType,
                    ExecReturnType = "StdResponse<" + method.OkResponseDataValueObject + ">",
                    NamespacePrefix = applicationArgumentRegistry.NamespacePrefix,
                    ExecuteParameterType = executeParameterType
                };
                var fileNameWithReturn = interfaceNameWithReturn + GetFileExtension();

                generatedInterface = templateService.RenderTemplate(interfaceTemplatePath, context);
                files.Add(new File(destinationFolder, fileNameWithReturn, generatedInterface));
                logger.LogInformation(
                    "    - {destinationFolder}/{fileNameWithReturn}",
                    destinationFolder,
                    fileNameWithReturn
                );
            }
        }

        return files;
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
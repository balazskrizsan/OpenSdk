using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices;

public class InterfaceGeneratorService : IInterfaceGeneratorService
{
    private readonly IFileGeneratorService fileGeneratorService;
    private readonly ITemplateService templateService;
    private readonly ILogger<InterfaceGeneratorService> logger;

    public InterfaceGeneratorService(
        IFileGeneratorService fileGeneratorService,
        ITemplateService templateService,
        ILogger<InterfaceGeneratorService> logger
    )
    {
        this.fileGeneratorService = fileGeneratorService;
        this.templateService = templateService;
        this.logger = logger;
    }

    public void Generate(List<Method> methods)
    {
        var interfaceTemplatePath = @"./templates/Interface.tpl";
        var namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_interfaces";
        var destinationFolder = "/" + namespaceValue.Replace(".", "/");

        foreach (var method in methods)
        {
            var interfaceName = "I" + method.MethodName;

            var context = new
            {
                InterfaceName = interfaceName,
                Namespace = namespaceValue,
                ParamObjectClassName = method.ParamObjectName,
                ParamObjectVarName = StringService.LowercaseFirst(method.ParamObjectName),
                MethodUri = method.Uri,
                MethodType = method.MethodType,
                ExecReturnType = "void"
            };
            var fileName = interfaceName + ".java";

            var generatedInterface = templateService.GenerateTemplate(interfaceTemplatePath, context);
            fileGeneratorService.SaveFile(destinationFolder, fileName, generatedInterface);

            if (!String.IsNullOrWhiteSpace(method.OkResponseValueObject))
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
                    ExecReturnType = "StdResponse<" + method.OkResponseDataValueObject + ">"
                };
                var fileNameWithReturn = interfaceNameWithReturn + ".java";

                generatedInterface = templateService.GenerateTemplate(interfaceTemplatePath, context);
                fileGeneratorService.SaveFile(destinationFolder, fileNameWithReturn, generatedInterface);
            }
        }
    }
}
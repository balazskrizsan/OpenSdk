using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices;

public class InterfaceGeneratorService : IInterfaceGeneratorService
{
    private readonly ITemplateService templateService;
    private readonly ILogger<InterfaceGeneratorService> logger;

    public InterfaceGeneratorService(
        ITemplateService templateService,
        ILogger<InterfaceGeneratorService> logger
    )
    {
        this.templateService = templateService;
        this.logger = logger;
    }

    public List<File> GenerateFiles(List<Method> methods)
    {
        var interfaceTemplatePath = @"./templates/Interface.liquid";
        var namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_interfaces";
        var destinationFolder = "\\" + namespaceValue.Replace(".", "\\");

        var files = new List<File>();
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
                    ExecReturnType = "StdResponse<" + method.OkResponseDataValueObject + ">"
                };
                var fileNameWithReturn = interfaceNameWithReturn + ".java";

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
}
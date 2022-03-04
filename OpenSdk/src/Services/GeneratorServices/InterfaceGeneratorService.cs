using System;
using System.Collections.Generic;
using System.IO;
using Fluid;
using Microsoft.Extensions.Logging;
using OpenSdk.Factories;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices
{
    public class InterfaceGeneratorService : IInterfaceGeneratorService
    {
        private readonly IFileGeneratorService fileGeneratorService;
        private readonly IFluidFactory fluidFactory;
        private readonly ILogger<InterfaceGeneratorService> logger;

        public InterfaceGeneratorService(
            IFileGeneratorService fileGeneratorService,
            IFluidFactory fluidFactory,
            ILogger<InterfaceGeneratorService> logger
        )
        {
            this.fileGeneratorService = fileGeneratorService;
            this.fluidFactory = fluidFactory;
            this.logger = logger;
        }

        public void Generate(List<Method> methods)
        {
            var interfaceTemplate = new StreamReader(@"./templates/Interface.tpl").ReadToEnd();

            var parser = fluidFactory.Create();
            foreach (var method in methods)
            {
                var namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_interfaces";
                var interfaceName = "I" + method.MethodName;

                var context = new TemplateContext(new
                {
                    InterfaceName = interfaceName,
                    Namespace = namespaceValue,
                    ParamObjectClassName = method.ParamObjectName,
                    ParamObjectVarName = StringService.LowercaseFirst(method.ParamObjectName),
                    MethodUri = method.Uri,
                    MethodType = method.MethodType,
                    ExecReturnType = "void"
                });

                var destinationFolder = "/" + namespaceValue.Replace(".", "/");
                var fileName = interfaceName + ".java";

                if (parser.TryParse(interfaceTemplate, out var template, out var error))
                {
                    var renderedTemplate = template.Render(context);
                    fileGeneratorService.SaveFile(destinationFolder, fileName, renderedTemplate);
                }
                else
                {
                    logger.LogError("Interface generator error: {}", error);
                }

                if (!String.IsNullOrEmpty(method.OkResponseValueObject))
                {
                    namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_interfaces";
                    interfaceName = "I" + method.MethodName + "WithReturn";

                    context = new TemplateContext(new
                    {
                        InterfaceName = interfaceName,
                        Namespace = namespaceValue,
                        ParamObjectClassName = method.ParamObjectName,
                        ParamObjectVarName = StringService.LowercaseFirst(method.ParamObjectName),
                        MethodUri = method.Uri,
                        MethodType = method.MethodType,
                        ExecReturnType = "OpenSdkStdResponse<" + method.OkResponseDataValueObject + ">"
                    });

                    destinationFolder = "/" + namespaceValue.Replace(".", "/");
                    fileName = interfaceName + ".java";

                    if (parser.TryParse(interfaceTemplate, out template, out error))
                    {
                        var renderedTemplate = template.Render(context);
                        fileGeneratorService.SaveFile(destinationFolder, fileName, renderedTemplate);
                    }
                    else
                    {
                        logger.LogError("Interface generator error: {}", error);
                    }
                }
            }
        }
    }
}
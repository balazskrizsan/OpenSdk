using System.Collections.Generic;
using System.IO;
using Cottle;
using OpenSdk.Factories;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices
{
    public class InterfaceGeneratorService : IInterfaceGeneratorService
    {
        private readonly IFileGeneratorService fileGeneratorService;
        private readonly ICottleFactory cottleFactory;

        public InterfaceGeneratorService(IFileGeneratorService fileGeneratorService, ICottleFactory cottleFactory)
        {
            this.fileGeneratorService = fileGeneratorService;
            this.cottleFactory = cottleFactory;
        }

        public void Generate(List<Method> methods)
        {
            var interfaceTemplate = new StreamReader(@"./templates/Interface.tpl").ReadToEnd();

            var templateDocument = cottleFactory.CreateDocument(interfaceTemplate);
            foreach (var method in methods)
            {
                var namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_interfaces";
                var interfaceName = "I" + method.MethodName;

                var context = Context.CreateBuiltin(new Dictionary<Value, Value>
                {
                    ["interfaceName"] = interfaceName,
                    ["namespace"] = namespaceValue,
                    ["paramObjectClassName"] = method.ParamObjectName,
                    ["paramObjectVarName"] = StringService.LowercaseFirst(method.ParamObjectName),
                    ["methodUri"] = method.Uri,
                    ["methodType"] = method.MethodType
                });

                var destinationFolder = "/" + namespaceValue.Replace(".", "/");
                var fileName = interfaceName + ".java";

                fileGeneratorService.SaveFile(destinationFolder, fileName, templateDocument.Render(context));
            }
        }
    }
}
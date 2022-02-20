using System;
using System.Collections.Generic;
using System.IO;
using Cottle;
using Microsoft.Extensions.Logging;
using OpenSdk.Factories;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ICottleFactory cottleFactory;
        private readonly ILogger<ParserService> logger;

        public GeneratorService(ICottleFactory cottleFactory, ILogger<ParserService> logger)
        {
            this.cottleFactory = cottleFactory;
            this.logger = logger;
        }

        public void Generate(ParserResponse openapiValues)
        {
            // var interfaceTemplate = File.ReadAllText(@"w:\\Interface.tpl");
            var interfaceTemplate = new StreamReader(@"./../../../templates/Interface.tpl").ReadToEnd();

            logger.LogInformation("====== Generate API Interfaces");

            var templateDocument = cottleFactory.CreateDocument(interfaceTemplate);
            foreach (var method in openapiValues.Methods)
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

                var destinationFolder = "W:/Cs/OpenSdkOutputTest/" + namespaceValue.Replace(".", "/");
                var fileName = interfaceName + ".java";

                SaveFile(destinationFolder, fileName, templateDocument.Render(context));
            }

            logger.LogInformation("====== Generate Value Objects");
            var valueObjectTemplate = new StreamReader(@"./../../../templates/ValueObject.tpl").ReadToEnd();
            templateDocument = cottleFactory.CreateDocument(valueObjectTemplate);

            foreach (Schema schema in openapiValues.Schemas)
            {
                var namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_parameter_objects";
                var valueObjectName = schema.Name;

                Dictionary<Value, Value> templateParams = new Dictionary<Value, Value>();
                foreach (var parameter in schema.Parameters)
                {
                    templateParams.Add(VarNameMapper(parameter.Key), TypeMapper(parameter.Value));
                }

                var context = Context.CreateBuiltin(new Dictionary<Value, Value>
                {
                    ["namespaceValue"] = schema.Name,
                    ["valueObjectName"] = valueObjectName,
                    ["parameters"] = templateParams,
                });

                var destinationFolder = "W:/Cs/OpenSdkOutputTest/" + namespaceValue.Replace(".", "/");
                var fileName = valueObjectName + ".java";

                SaveFile(destinationFolder, fileName, templateDocument.Render(context));
            }

            logger.LogInformation("====== Successful finish");
        }

        private void SaveFile(string destinationFolder, string fileName, string content)
        {
            Directory.CreateDirectory(destinationFolder);
            File.WriteAllTextAsync(destinationFolder + "/" + fileName, content);
            logger.LogInformation("    - " + destinationFolder + "/" + fileName);
        }

        private string TypeMapper(string openapiType)
        {
            switch (openapiType)
            {
                case "string":
                    return "String";
                case "#/components/schemas/FileUpload":
                    return "HttpEntity<ByteArrayResource>";
                default:
                    throw new Exception("No type found for: " + openapiType);
            }
        }

        private string VarNameMapper(string varName)
        {
            switch (varName)
            {
                case "#/components/schemas/FileUpload":
                    return "content";
                default:
                    return varName;
            }
        }
    }
}
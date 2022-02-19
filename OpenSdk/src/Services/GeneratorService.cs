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

        public GeneratorService(
            ICottleFactory cottleFactory,
            ILogger<ParserService> logger
            )
        {
            this.cottleFactory = cottleFactory;
            this.logger = logger;
        }

        public void Generate(ParserResponse openapiValues)
        {
            var interfaceTemplate = File.ReadAllText(@"w:\\Interface.tpl");
            var valueObjectTemplate = File.ReadAllText(@"w:\\ValueObject.tpl");

            logger.LogInformation("=====================================================");

            foreach (var method in openapiValues.Methods)
            {
                var context = Context.CreateBuiltin(new Dictionary<Value, Value>
                {
                    ["interfaceName"] = method.MethodName,
                    ["paramObjectClassName"] = method.ParamObjectName,
                    ["paramObjectVarName"] = StringService.LowercaseFirst(method.ParamObjectName),
                    ["methodUri"] = method.Uri,
                    ["methodType"] = method.MethodType
                });
                logger.LogInformation(cottleFactory.CreateDocument(interfaceTemplate).Render(context));
            }

            logger.LogInformation("=====================================================");
            foreach (Schema schema in openapiValues.Schemas)
            {
                Dictionary<Value, Value> templateParams = new Dictionary<Value, Value>();
                foreach (var parameter in schema.Parameters)
                {
                    templateParams.Add(VarNameMapper(parameter.Key), TypeMapper(parameter.Value));
                }
            
                var context = Context.CreateBuiltin(new Dictionary<Value, Value>
                {
                    ["objectName"] = schema.Name,
                    ["parameters"] = templateParams,
                });
                logger.LogInformation(cottleFactory.CreateDocument(valueObjectTemplate).Render(context));
            }
            
            logger.LogInformation("=====================================================");
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
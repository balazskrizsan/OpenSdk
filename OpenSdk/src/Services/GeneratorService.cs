using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cottle;
using OpenSdk.Factories;
using OpenSdk.ValueObjects;

namespace OpenSdk.Services
{
    public class GeneratorService : IGeneratorService
    {
        private readonly ICottleFactory cottleFactory;

        public GeneratorService(ICottleFactory cottleFactory)
        {
            this.cottleFactory = cottleFactory;
        }

        public void Generate(ParserResponse openapiValues)
        {
            var interfaceTemplate = File.ReadAllText(@"w:\\Interface.tpl");
            var valueObjectTemplate = File.ReadAllText(@"w:\\ValueObject.tpl");

            Console.WriteLine("=====================================================");

            foreach (var method in openapiValues.Methods)
            {
                var context = Context.CreateBuiltin(new Dictionary<Value, Value>
                {
                    ["interfaceName"] = method.MethodName,
                    ["paramObjectClassName"] = method.ParamObjectName,
                    ["paramObjectVarName"] = StringService.LowercaseFirst(method.ParamObjectName),
                    ["methodUri"] = method.Uri,
                });
                Console.WriteLine(cottleFactory.CreateDocument(interfaceTemplate).Render(context));
            }

            Console.WriteLine("=====================================================");
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
                Console.WriteLine(cottleFactory.CreateDocument(valueObjectTemplate).Render(context));
            }

            Console.WriteLine("=====================================================");
        }

        private string TypeMapper(string openapiType)
        {
            switch (openapiType)
            {
                case "string":
                    return "String";
                case "#/components/schemas/FileUpload":
                    return "FileUpload";
                default:
                    throw new Exception("No type found for: " + openapiType);
            }
        }

        private string VarNameMapper(string varName)
        {
            switch (varName)
            {
                case "#/components/schemas/FileUpload":
                    return "fileUpload";
                default:
                    return varName;
            }
        }
    }
}

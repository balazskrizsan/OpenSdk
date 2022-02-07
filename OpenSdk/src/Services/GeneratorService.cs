using System;
using System.Collections.Generic;
using System.IO;
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


            // Console.Write(cottleFactory.CreateDocument(valueObjectTemplate).Render(context));
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            foreach (var method in openapiValues.Methods)
            {
                var context = Context.CreateBuiltin(new Dictionary<Value, Value>
                {
                    ["interfaceName"] = method.MethodName,
                    ["paramObjectClassName"] = method.ParamObjectName,
                    ["paramObjectVarName"] = StringService.LowercaseFirst(method.ParamObjectName),
                    ["methodUri"] = method.Uri,
                });
                Console.Write(cottleFactory.CreateDocument(interfaceTemplate).Render(context));
            }
        }
    }
}

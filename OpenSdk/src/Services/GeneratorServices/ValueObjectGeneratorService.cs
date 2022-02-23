using System.Collections.Generic;
using System.IO;
using Cottle;
using Microsoft.Extensions.Logging;
using OpenSdk.Factories;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices
{
    public class ValueObjectGeneratorService : IValueObjectGeneratorService
    {
        private readonly ILogger<ParserService> logger;
        private readonly ICottleFactory cottleFactory;
        private readonly IFileGeneratorService fileGeneratorService;

        public ValueObjectGeneratorService(
            ILogger<ParserService> logger,
            ICottleFactory cottleFactory,
            IFileGeneratorService fileGeneratorService
        )
        {
            this.logger = logger;
            this.cottleFactory = cottleFactory;
            this.fileGeneratorService = fileGeneratorService;
        }

        public void Generate(List<Schema> schemas)
        {
            var valueObjectTemplate = new StreamReader(@"./templates/ValueObject.tpl").ReadToEnd();
            var templateDocument = cottleFactory.CreateDocument(valueObjectTemplate);

            foreach (Schema schema in schemas)
            {
                var namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_parameter_objects";
                var valueObjectName = schema.Name;

                Dictionary<Value, Value> templateParams = new Dictionary<Value, Value>();
                foreach (var parameter in schema.Parameters)
                {
                    templateParams.Add(
                        fileGeneratorService.VarNameMapper(parameter.Key),
                        fileGeneratorService.TypeMapper(parameter.Value)
                    );
                }

                var context = Context.CreateBuiltin(new Dictionary<Value, Value>
                {
                    ["namespaceValue"] = namespaceValue,
                    ["valueObjectName"] = valueObjectName,
                    ["parameters"] = templateParams,
                });

                var destinationFolder = "W:/Cs/OpenSdkOutputTest/" + namespaceValue.Replace(".", "/");
                var fileName = valueObjectName + ".java";

                fileGeneratorService.SaveFile(destinationFolder, fileName, templateDocument.Render(context));
            }
        }
    }
}

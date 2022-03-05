using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices;

public class ValueObjectGeneratorService : IValueObjectGeneratorService
{
    private readonly ILogger<ValueObjectGeneratorService> logger;
    private readonly ITemplateService templateService;
    private readonly IFileGeneratorService fileGeneratorService;

    public ValueObjectGeneratorService(
        ILogger<ValueObjectGeneratorService> logger,
        ITemplateService templateService,
        IFileGeneratorService fileGeneratorService
    )
    {
        this.logger = logger;
        this.templateService = templateService;
        this.fileGeneratorService = fileGeneratorService;
    }

    public void Generate(List<Schema> schemas)
    {
        var templatePath = @"./templates/ValueObject.tpl";
        var namespaceValue = "com.kbalazsworks.stackjudge_aws_sdk.schema_parameter_objects";

        foreach (Schema schema in schemas)
        {
            var valueObjectName = schema.Name;

            var parameters = new List<KeyValuePair<string, string>>();
            foreach (var parameter in schema.Parameters)
            {
                parameters.Add(new KeyValuePair<string, string>(
                    fileGeneratorService.TypeMapper(parameter.Value),
                    fileGeneratorService.VarNameMapper(parameter.Key)
                ));
            }

            // @todo: run a component check:
            //  - if schema is a response it should not implement the IOpenSdk[Post|Get|Put|Delete]able
            //  - add condition around the IOpenSdk*
            var context = new
            {
                NamespaceValue = namespaceValue,
                ValueObjectName = valueObjectName,
                Parameters = parameters,
            };

            var destinationFolder = "/" + namespaceValue.Replace(".", "/");
            var fileName = valueObjectName + ".java";

            var generatedValueObject = templateService.GenerateTemplate(templatePath, context);
            fileGeneratorService.SaveFile(destinationFolder, fileName, generatedValueObject);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenSdk.Constants;
using OpenSdk.Registries;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices;

public class ValueObjectGeneratorService : IValueObjectGeneratorService
{
    private readonly ITemplateService templateService;
    private readonly IMapperService mapperService;
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;
    private readonly ILogger<ValueObjectGeneratorService> logger;

    public ValueObjectGeneratorService(
        ITemplateService templateService,
        IMapperService mapperService,
        IApplicationArgumentRegistry applicationArgumentRegistry,
        ILogger<ValueObjectGeneratorService> logger
    )
    {
        this.templateService = templateService;
        this.mapperService = mapperService;
        this.applicationArgumentRegistry = applicationArgumentRegistry;
        this.logger = logger;
    }

    public List<File> GetGeneratedFiles(List<Schema> schemas)
    {
        var templatePath = @"./templates/ValueObjectLombok.liquid";
        var namespaceValue = applicationArgumentRegistry.NamespacePrefix + ".schema_parameter_objects";

        var files = new List<File>();
        foreach (Schema schema in schemas)
        {
            var valueObjectName = schema.Name;
            var isResponseObject = IsResponseObject(schema.Parameters);
            var implementation = GetImplementation(schema, isResponseObject);

            var parameters = new List<KeyValuePair<string, ValueObjectProperty>>();
            foreach (var parameter in schema.Parameters)
            {
                parameters.Add(new KeyValuePair<string, ValueObjectProperty>(
                    mapperService.TypeMapper(parameter.Value),
                    new ValueObjectProperty(
                        mapperService.VarNameMapper(parameter.Key),
                        GetJsonPropertyValue(parameter.Key, isResponseObject)
                    )
                ));
            }

            // @todo: run a component check:
            //  - if schema is a response it should not implement the IOpenSdk[Post|Get|Put|Delete]able
            //  - add condition around the IOpenSdk*
            var context = new
            {
                Implementation = implementation,
                NamespaceValue = namespaceValue,
                ValueObjectName = valueObjectName,
                Parameters = parameters,
                NamespacePrefix = applicationArgumentRegistry.NamespacePrefix
            };

            var destinationFolder = "\\" + namespaceValue.Replace(".", "\\");
            var fileName = valueObjectName + ".java";

            var generatedValueObject = templateService.RenderTemplate(templatePath, context);
            files.Add(new File(destinationFolder, fileName, generatedValueObject));
            logger.LogInformation("    - {destinationFolder}/{fileName} ", destinationFolder, fileName);
        }

        return files;
    }

    private string GetImplementation(Schema schema, bool isResponseObject)
    {
        if (isResponseObject)
        {
            return string.Empty;
        }
        //@todo: add condition if it's a postable object

        return "IOpenSdkPostable";
    }

    private string GetJsonPropertyValue(string parameterKey, bool isResponseObject)
    {
        if (!isResponseObject)
        {
            return parameterKey;
        }

        if (ResponseEntityPropertyConst.AsListWithoutData().Contains(parameterKey))
        {
            return parameterKey;
        }

        return ResponseEntityPropertyConst.DATA;
    }

    private bool IsResponseObject(Dictionary<string, string> schemaParameters)
    {
        var foundResponseEntityKeys = schemaParameters
            .Keys
            .Count(x =>
                x.Contains(ResponseEntityPropertyConst.SUCCESS) ||
                x.Contains(ResponseEntityPropertyConst.ERROR_CODE) ||
                x.Contains(ResponseEntityPropertyConst.REQUEST_ID)
            );

        return foundResponseEntityKeys == 3;
    }
}
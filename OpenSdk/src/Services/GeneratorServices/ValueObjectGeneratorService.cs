using System;
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
        var templatePath = GetTemplatePath();
        var namespaceValue = applicationArgumentRegistry.NamespacePrefix + ".schema_parameter_objects";

        var files = new List<File>();
        foreach (Schema schema in schemas)
        {
            var valueObjectName = schema.Name;
            var isResponseObject = IsResponseObject(schema.Parameters);
            var implementations = GetImplementations(schema, isResponseObject);

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
                Implementations = implementations,
                IsGetable = schema.HasGet,
                IsPostable = schema.HasPost,
                NamespaceValue = namespaceValue,
                ValueObjectName = valueObjectName,
                Parameters = parameters,
                NamespacePrefix = applicationArgumentRegistry.NamespacePrefix
            };

            var destinationFolder = "\\" + namespaceValue.Replace(".", "\\");
            var fileName = valueObjectName + GetFileExtension();

            var generatedValueObject = templateService.RenderTemplate(templatePath, context);
            files.Add(new File(destinationFolder, fileName, generatedValueObject));
            logger.LogInformation("    - {destinationFolder}/{fileName} ", destinationFolder, fileName);
        }

        return files;
    }

    private string GetImplementations(Schema schema, bool isResponseObject)
    {
        var interfaces = new List<string>();
        var hasInterface = false;

        if (schema.HasGet)
        {
            hasInterface = true;
            interfaces.Add(FileNameConsts.I_OPENSDK_GETABLE);
        }

        if (schema.HasPost)
        {
            hasInterface = true;
            interfaces.Add(FileNameConsts.I_OPENSDK_POSTABLE);
        }

        if (!hasInterface)
        {
            return null;
        }

        return string.Join(", ", interfaces.ToArray());
    }

    private string GetTemplatePath()
    {
        var language = applicationArgumentRegistry.OutputLanguage;

        switch (language)
        {
            case LanguagesConsts.JAVA: return @"./templates/ValueObject.Java.Lombok.liquid";
            case LanguagesConsts.TYPE_SCRIPT: return @"./templates/ValueObject.TypeScript.liquid";
            default: throw new Exception("ValueObject language template not existing: " + language);
        }
    }

    private string GetFileExtension()
    {
        var language = applicationArgumentRegistry.OutputLanguage;

        switch (language)
        {
            case LanguagesConsts.JAVA: return FileExtensionConsts.JAVA;
            case LanguagesConsts.TYPE_SCRIPT: return FileExtensionConsts.TYPESCRTIPY;
            default: throw new Exception("File extension is not supported: " + language);
        }
    }

    private string GetJsonPropertyValue(string parameterKey, bool isResponseObject)
    {
        if (!isResponseObject)
        {
            return parameterKey;
        }

        if (ResponseEntityPropertyConsts.AsListWithoutData().Contains(parameterKey))
        {
            return parameterKey;
        }

        return ResponseEntityPropertyConsts.DATA;
    }

    private bool IsResponseObject(Dictionary<string, string> schemaParameters)
    {
        var foundResponseEntityKeys = schemaParameters
            .Keys
            .Count(x =>
                x.Contains(ResponseEntityPropertyConsts.SUCCESS) ||
                x.Contains(ResponseEntityPropertyConsts.ERROR_CODE) ||
                x.Contains(ResponseEntityPropertyConsts.REQUEST_ID)
            );

        return foundResponseEntityKeys == 3;
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenSdk.Constants;
using OpenSdk.Registries;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services;

public class LanguageSpecificGeneratorService : ILanguageSpecificGeneratorService
{
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;
    private readonly ITemplateService templateService;
    private readonly ILogger<ValueObjectGeneratorService> logger;

    public LanguageSpecificGeneratorService(
        IApplicationArgumentRegistry applicationArgumentRegistry,
        ITemplateService templateService,
        ILogger<ValueObjectGeneratorService> logger
    )
    {
        this.applicationArgumentRegistry = applicationArgumentRegistry;
        this.templateService = templateService;
        this.logger = logger;
    }

    public IEnumerable<File> GetSpecificFilesByFiles(List<File> files)
    {
        List<File> extraFiles = new List<File>();

        if (applicationArgumentRegistry.OutputLanguage == "TypeScript")
        {
            var valueObjects = files.Where(f => f.FileTypeId == GeneratedFileTypeConsts.VALUE_OBJECT);

            List<string> classNames = new List<string>();
            foreach (var valueObject in valueObjects)
            {
                classNames.Add(valueObject.ClassName);
            }

            var context = new
            {
                ClassNames = classNames
            };

            extraFiles.Add(new File(
                valueObjects.First().DestinationFolder,
                "index.ts",
                "",
                templateService.RenderTemplate(
                    "./templates/ValueObject.TypeScript.DirectoryIndex.liquid",
                    context
                ),
                GeneratedFileTypeConsts.TS_FOLDER_INDEX
            ));
            logger.LogInformation("    - {destinationFolder}\\{fileName} ", valueObjects.First().DestinationFolder, "index.ts");
        }

        return extraFiles;
    }
}
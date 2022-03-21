using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenSdk.Registries;
using File = OpenSdk.ValueObjects.File;

namespace OpenSdk.Services;

public class FileService : IFileService
{
    private readonly ILogger<ParserService> logger;
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;

    public FileService(
        ILogger<ParserService> logger,
        IApplicationArgumentRegistry applicationArgumentRegistry
    )
    {
        this.logger = logger;
        this.applicationArgumentRegistry = applicationArgumentRegistry;
    }

    public void SaveFiles(List<File> files)
    {
        Task.WaitAll(files.Select(file => Task.Factory.StartNew(() => SaveFile(file))).ToArray());
    }

    private void SaveFile(File file)
    {
        var destinationFolder = applicationArgumentRegistry.OutputFolder + file.DestinationFolder;
        Directory.CreateDirectory(destinationFolder);
        System.IO.File.WriteAllTextAsync(destinationFolder + "\\" + file.FileName, file.Content);

        logger.LogInformation("    - " + destinationFolder + "\\" + file.FileName);
    }
}
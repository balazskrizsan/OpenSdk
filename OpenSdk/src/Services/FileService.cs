using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenSdk.Registries;
using File = OpenSdk.ValueObjects.Parser.File;

namespace OpenSdk.Services;

public class FileService : IFileService
{
    private readonly ILogger<FileService> logger;
    private readonly IApplicationArgumentRegistry applicationArgumentRegistry;

    public FileService(
        ILogger<FileService> logger,
        IApplicationArgumentRegistry applicationArgumentRegistry
    )
    {
        this.logger = logger;
        this.applicationArgumentRegistry = applicationArgumentRegistry;
    }

    public void SaveFilesAsync(List<File> files)
    {
        var tasks = new List<Task>();
        foreach (var file in files)
        {
            tasks.Add(Task.Factory.StartNew(() => SaveFile(file)));
        }

        Task.WaitAll(tasks.ToArray());
    }

    private void SaveFile(File file)
    {
        var destinationFolder = applicationArgumentRegistry.OutputFolder + file.DestinationFolder;
        Directory.CreateDirectory(destinationFolder);
        System.IO.File.WriteAllTextAsync(destinationFolder + "\\" + file.FileName, file.Content);

        logger.LogInformation("    - " + destinationFolder + "\\" + file.FileName);
    }
}
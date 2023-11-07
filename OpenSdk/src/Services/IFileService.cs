using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services;

public interface IFileService
{
    void SaveFilesAsync(List<File> files);
}
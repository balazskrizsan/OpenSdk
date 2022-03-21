using System.Collections.Generic;
using OpenSdk.ValueObjects;

namespace OpenSdk.Services;

public interface IFileService
{
    void SaveFiles(List<File> files);
}
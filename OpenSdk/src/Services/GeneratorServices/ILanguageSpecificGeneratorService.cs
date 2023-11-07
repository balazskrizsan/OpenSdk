using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services;

public interface ILanguageSpecificGeneratorService
{
    IEnumerable<File> GetSpecificFilesByFiles(List<File> files);
}
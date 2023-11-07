using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;

namespace OpenSdk.Services;

public interface IValueObjectGeneratorService
{
    List<File> GetGeneratedFiles(List<Schema> schemas);
}
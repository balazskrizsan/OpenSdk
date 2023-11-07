using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;
using OpenSdk.ValueObjects.Parser.Generator;

namespace OpenSdk.Services;

public interface IInterfaceGeneratorService
{
    List<File> GetGenerateFiles(List<UriMethods> uriMethods);
}
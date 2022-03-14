using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices;

public interface IInterfaceGeneratorService
{
    List<File> GenerateFiles(List<Method> methods);
}
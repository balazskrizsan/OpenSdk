using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices
{
    public interface IValueObjectGeneratorService
    {
        List<File> GetGeneratedFiles(List<Schema> schemas);
    }
}
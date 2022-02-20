using System.Collections.Generic;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices
{
    public interface IValueObjectGeneratorService
    {
        public void Generate(List<Schema> openapiValuesSchemas);
    }
}
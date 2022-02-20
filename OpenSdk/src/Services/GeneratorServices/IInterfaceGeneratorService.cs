using System.Collections.Generic;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.Services.GeneratorServices
{
    public interface IInterfaceGeneratorService
    {
        public void Generate(List<Method> methods);
    }
}
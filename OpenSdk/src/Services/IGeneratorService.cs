using OpenSdk.ValueObjects;

namespace OpenSdk.Services
{
    public interface IGeneratorService
    {
        void Generate(ParserResponse parserResponse);
    }
}

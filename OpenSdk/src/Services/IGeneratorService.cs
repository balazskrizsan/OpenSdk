using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services
{
    public interface IGeneratorService
    {
        void Generate(ParserResponse parserResponse);
    }
}

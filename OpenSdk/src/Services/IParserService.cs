using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services
{
    public interface IParserService
    {
        ParserResponse Parse(string dataSourcePath);
    }
}

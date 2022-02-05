using OpenSdk.ValueObjects;

namespace OpenSdk.Services
{
    public interface IParserService
    {
        ParserResponse Parse(string dataSourcePath);
    }
}

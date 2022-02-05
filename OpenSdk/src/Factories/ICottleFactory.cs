using Cottle;

namespace OpenSdk.Factories
{
    public interface ICottleFactory
    {
        IDocument CreateDocument(string template);
    }
}

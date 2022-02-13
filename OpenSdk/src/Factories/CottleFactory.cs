using Cottle;

namespace OpenSdk.Factories
{
    public class CottleFactory : ICottleFactory
    {
        public IDocument CreateDocument(string template)
        {
            var documentResult = Document.CreateDefault(
                template,
                new DocumentConfiguration
                {
                    BlockBegin = "{{",
                    BlockEnd = "}}"
                }
            );

            return documentResult.DocumentOrThrow;
        }
    }
}

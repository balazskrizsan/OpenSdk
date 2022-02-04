using OpenSdk.Services;

namespace OpenSdk
{
    public class Bootstrap : IBootstrap
    {
        private readonly IParserService parserService;

        public Bootstrap(IParserService parserService)
        {
            this.parserService = parserService;
        }

        public void Start(string dataSourcePath)
        {
            parserService.Parse(dataSourcePath);
        }
    }
}

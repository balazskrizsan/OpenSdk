using OpenSdk.Services;

namespace OpenSdk
{
    public class Bootstrap
    {
        private ParserService parserService;

        public Bootstrap(string[] args, ParserService parserService)
        {
            this.parserService = parserService;
        }

        public void Start()
        {
            parserService.Parse();
        }
    }
}

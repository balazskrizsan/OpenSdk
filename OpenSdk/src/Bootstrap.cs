using OpenSdk.Registries;
using OpenSdk.Services;

namespace OpenSdk
{
    public class Bootstrap : IBootstrap
    {
        private readonly IParserService parserService;
        private readonly IApplicationArgumentRegistry applicationArgumentRegistry;

        public Bootstrap(IParserService parserService, IApplicationArgumentRegistry applicationArgumentRegistry)
        {
            this.parserService = parserService;
            this.applicationArgumentRegistry = applicationArgumentRegistry;
        }

        public void Start()
        {
            parserService.Parse(applicationArgumentRegistry.GetDataSourcePath());
        }
    }
}

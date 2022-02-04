using OpenSdk.Registries;
using OpenSdk.Services;

namespace OpenSdk
{
    public class Bootstrap : IBootstrap
    {
        private readonly IParserService parserService;
        private readonly ApplicationArgumentRegistry applicationArgumentRegistry;

        public Bootstrap(IParserService parserService, IApplicationArgumentRegistry applicationArgumentRegistry)
        {
            this.parserService = parserService;
            this.applicationArgumentRegistry = applicationArgumentRegistry as ApplicationArgumentRegistry;
        }

        public void Start()
        {
            parserService.Parse(applicationArgumentRegistry.DataSourcePath);
        }
    }
}

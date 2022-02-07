using Microsoft.Extensions.DependencyInjection;

namespace OpenSdk
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataSourcePath = @"c:\Repos\OpenSdk\sample_api.yml";

            new ServiceCollection()
             .Configure(dataSourcePath)
             .BuildServiceProvider()
             .GetService<IBootstrap>()
             ?.Start();
        }
    }
}

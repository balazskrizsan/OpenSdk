using System;
using OpenSdk.Services;
using OpenSdk.Services.ParserServices;

namespace OpenSdk
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataSourcePath = args[0];

            Bootstrap bootstrap = new Bootstrap(
                args,
                new ParserService(
                    new DataSourceService(dataSourcePath),
                    new PathsParserService(),
                    new ComponentsParserService()
                )
            );
            bootstrap.Start();
        }
    }
}
using OpenSdk.Services;

namespace OpenSdk
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap bootstrap = new Bootstrap(args, new ParserService(new DataSourceService()));
            bootstrap.Start();
        }
    }
}

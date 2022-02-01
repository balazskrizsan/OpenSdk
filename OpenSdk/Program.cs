using System;
using OpenSdk.Services;

namespace OpenSdk
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine(args[0]);
            Bootstrap bootstrap = new Bootstrap(args, new ParserService(new DataSourceService(args[0])));
            bootstrap.Start();
        }
    }
}

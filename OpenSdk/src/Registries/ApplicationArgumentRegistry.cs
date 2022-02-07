using System.ComponentModel;

namespace OpenSdk.Registries
{
    public class ApplicationArgumentRegistry : IApplicationArgumentRegistry
    {
        public string DataSourcePath { get; }

        public ApplicationArgumentRegistry(string dataSourcePath)
        {
            DataSourcePath = dataSourcePath;
        }
    }
}

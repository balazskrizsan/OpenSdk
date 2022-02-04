using System.ComponentModel;

namespace OpenSdk.Registries
{
    public class ApplicationArgumentRegistry : IApplicationArgumentRegistry
    {
        [ReadOnly(true)] public string DataSourcePath { get; }

        public ApplicationArgumentRegistry(string dataSourcePath)
        {
            DataSourcePath = dataSourcePath;
        }

        // @todo: find a way to accessible property by interface
        public string GetDataSourcePath()
        {
            return DataSourcePath;
        }
    }
}

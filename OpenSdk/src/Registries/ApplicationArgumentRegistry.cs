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
    }
}

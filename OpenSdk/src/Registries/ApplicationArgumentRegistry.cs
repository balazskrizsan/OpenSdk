namespace OpenSdk.Registries
{
    public class ApplicationArgumentRegistry : IApplicationArgumentRegistry
    {
        public string DataSourcePath { get; }
        public string OutputFolder { get; }

        public ApplicationArgumentRegistry(string dataSourcePath, string outputFolder)
        {
            DataSourcePath = dataSourcePath;
            OutputFolder = outputFolder;
        }
    }
}

namespace OpenSdk.Registries
{
    public class ApplicationArgumentRegistry : IApplicationArgumentRegistry
    {
        public string DataSourcePath { get; }
        public string OutputFolder { get; }
        public string NamespacePrefix { get; }
        public string OutputLanguage { get; }

        public ApplicationArgumentRegistry(
            string dataSourcePath,
            string outputFolder,
            string namespacePrefix,
            string outputLanguage
        )
        {
            DataSourcePath = dataSourcePath;
            OutputFolder = outputFolder;
            NamespacePrefix = namespacePrefix;
            OutputLanguage = outputLanguage;
        }
    }
}

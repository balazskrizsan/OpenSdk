namespace OpenSdk.Registries
{
    public interface IApplicationArgumentRegistry
    {
        string DataSourcePath { get; }
        string OutputFolder { get; }
        public string NamespacePrefix { get; }
        public string OutputLanguage { get; }
    }
}

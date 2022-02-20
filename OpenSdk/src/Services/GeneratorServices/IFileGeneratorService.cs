namespace OpenSdk.Services.GeneratorServices
{
    public interface IFileGeneratorService
    {
        void SaveFile(string destinationFolder, string fileName, string content);
        string TypeMapper(string openapiType);
        string VarNameMapper(string varName);
    }
}
namespace OpenSdk.Services.GeneratorServices
{
    public interface IFileGeneratorService
    {
        string TypeMapper(string openapiType);
        string VarNameMapper(string varName);
    }
}
namespace OpenSdk.Services.GeneratorServices;

public interface IMapperService
{
    string TypeMapper(string openapiType);
    string VarNameMapper(string varName);
}
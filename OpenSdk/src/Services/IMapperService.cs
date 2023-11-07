namespace OpenSdk.Services;

public interface IMapperService
{
    string TypeMapper(string openapiType, string generic = null);
    string VarNameMapper(string varName);
    bool IsPrimitive(string openapiType);
}
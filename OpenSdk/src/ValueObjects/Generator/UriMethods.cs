namespace OpenSdk.ValueObjects.Parser.Generator;

public class UriMethods
{
    public string Uri { get; }
    public string PathClassName { get; }
    public Method GetMethod { get; }
    public Method PostMethod { get; }

    public UriMethods(string uri, string pathClassName, Method getMethod, Method postMethod)
    {
        Uri = uri;
        PathClassName = pathClassName;
        GetMethod = getMethod;
        PostMethod = postMethod;
    }
}
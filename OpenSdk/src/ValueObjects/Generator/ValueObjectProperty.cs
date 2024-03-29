#nullable enable
using DotLiquid;

namespace OpenSdk.ValueObjects.Parser.Generator;

public record ValueObjectProperty(string Value, string? JsonPropertyValue, string Key, bool IsValueObject) : ILiquidizable
{
    public object ToLiquid()
    {
        return new
        {
            Value = Value,
            JsonPropertyValue = JsonPropertyValue,
            Key = Key,
            IsValueObject = IsValueObject
        };
    }
}

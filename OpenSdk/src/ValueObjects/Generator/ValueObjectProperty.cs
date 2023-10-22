#nullable enable
using DotLiquid;

namespace OpenSdk.ValueObjects.Generator;

public record ValueObjectProperty(string Value, string? JsonPropertyValue, string Key) : ILiquidizable
{
    public object ToLiquid()
    {
        return new
        {
            Value = Value,
            JsonPropertyValue = JsonPropertyValue,
            Key = Key
        };
    }
}

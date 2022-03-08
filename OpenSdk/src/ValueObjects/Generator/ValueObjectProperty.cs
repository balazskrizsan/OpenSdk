#nullable enable
using DotLiquid;

namespace OpenSdk.ValueObjects.Generator;

public record ValueObjectProperty(string Value, string? JsonPropertyValue) : ILiquidizable
{
    public object ToLiquid()
    {
        return new
        {
            Value = Value,
            JsonPropertyValue = JsonPropertyValue
        };
    }
}

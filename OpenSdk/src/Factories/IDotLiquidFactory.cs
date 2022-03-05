using DotLiquid;

namespace OpenSdk.Factories;

public interface IDotLiquidFactory
{
    Template CreateTemplate(string template);
}
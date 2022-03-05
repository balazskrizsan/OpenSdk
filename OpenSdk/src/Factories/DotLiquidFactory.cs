using DotLiquid;

namespace OpenSdk.Factories;

public class DotLiquidFactory : IDotLiquidFactory
{
    public Template CreateTemplate(string template)
    {
        return Template.Parse(template);
    }
}
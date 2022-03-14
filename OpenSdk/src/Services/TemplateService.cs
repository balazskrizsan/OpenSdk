using System.IO;
using DotLiquid;
using OpenSdk.Factories;

namespace OpenSdk.Services;

public class TemplateService : ITemplateService
{
    private readonly IDotLiquidFactory dotLiquidFactory;

    public TemplateService(IDotLiquidFactory dotLiquidFactory)
    {
        this.dotLiquidFactory = dotLiquidFactory;
    }

    public string RenderTemplate(string templatePath, object context)
    {
        return dotLiquidFactory
            .CreateTemplate(new StreamReader(templatePath).ReadToEnd())
            .Render(Hash.FromAnonymousObject(context));
    }
}
using System.IO;
using DotLiquid;
using Microsoft.Extensions.Logging;
using OpenSdk.Services.GeneratorServices;

namespace OpenSdk.Services;

public class TemplateService : ITemplateService
{
    private readonly ILogger<InterfaceGeneratorService> logger;

    public TemplateService(ILogger<InterfaceGeneratorService> logger)
    {
        this.logger = logger;
    }

    public string GenerateTemplate(string templatePath, object context)
    {
        return Template
            .Parse(new StreamReader(templatePath).ReadToEnd())
            .Render(Hash.FromAnonymousObject(context));
    }
}
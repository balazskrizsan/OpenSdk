using System;
using System.IO;
using Fluid;
using Microsoft.Extensions.Logging;
using OpenSdk.Factories;
using OpenSdk.Services.GeneratorServices;

namespace OpenSdk.Services;

public class TemplateService : ITemplateService
{
    private readonly IFluidFactory fluidFactory;
    private readonly ILogger<InterfaceGeneratorService> logger;

    public TemplateService(
        IFluidFactory fluidFactory,
        ILogger<InterfaceGeneratorService> logger
    )
    {
        this.fluidFactory = fluidFactory;
        this.logger = logger;
    }

    public string GenerateTemplate(string templatePath, TemplateContext context)
    {
        var parser = fluidFactory.Create();

        var interfaceTemplate = new StreamReader(templatePath).ReadToEnd();

        if (parser.TryParse(interfaceTemplate, out var template, out var error))
        {
            return template.Render(context);
        }

        logger.LogError("Template generator error: {}", error);

        throw new Exception("Template generator error");
    }
}
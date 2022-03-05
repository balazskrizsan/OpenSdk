using Fluid;

namespace OpenSdk.Services;

public interface ITemplateService
{
    string GenerateTemplate(string templatePath, TemplateContext context);
}

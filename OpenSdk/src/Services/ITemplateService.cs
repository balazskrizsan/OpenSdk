namespace OpenSdk.Services;

public interface ITemplateService
{
    string GenerateTemplate(string templatePath, object context);
}
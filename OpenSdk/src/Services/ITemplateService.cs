namespace OpenSdk.Services;

public interface ITemplateService
{
    string RenderTemplate(string templatePath, object context);
}
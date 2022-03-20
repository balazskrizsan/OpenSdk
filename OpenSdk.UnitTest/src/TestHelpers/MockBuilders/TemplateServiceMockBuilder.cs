using NSubstitute;
using OpenSdk.Services;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class TemplateServiceMockBuilder
{
    public static ITemplateService CreateMock()
    {
        var mock = Substitute.For<ITemplateService>();

        //@todo: should not be here
        mock
            .RenderTemplate(Arg.Any<string>(), Arg.Any<object>())
            .Returns("generated template string");

        return mock;
    }
}
using Moq;
using OpenSdk.Services;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class TemplateServiceMockBuilder
{
    public static Mock<ITemplateService> GetMock()
    {
        var mock = new Mock<ITemplateService>();

        mock
            .Setup(s => s.RenderTemplate(It.IsAny<string>(), It.IsAny<object>()))
            .Returns("generated template string");

        return mock;
    }
}
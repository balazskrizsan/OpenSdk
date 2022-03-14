using NUnit.Framework;
using OpenSdk.Factories;
using OpenSdk.Services;

namespace OpenSdk.UnitTest.Services.TemplateServiceTest;

public class RenderTemplateTest
{
    [Test]
    public void Render_Perfect()
    {
        // Arrange
        var testedTemplate = "test_templates/test.liquid";
        var testedContext = new
        {
            TestVar = "context test"
        };
        var expectedTemplate = "unit context test test";

        // Act
        var actual = new TemplateService(new DotLiquidFactory()).RenderTemplate(testedTemplate, testedContext);

        // Assert
        Assert.AreEqual(expectedTemplate, actual);
    }
}
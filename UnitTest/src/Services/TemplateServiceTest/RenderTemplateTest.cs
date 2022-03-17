using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSdk.Factories;
using OpenSdk.Services;

namespace OpenSdk.UnitTest.Services.TemplateServiceTest;

[TestClass]
public class RenderTemplateTest
{
    [TestMethod]
    public void RenderSimpleTemplate_TestContextAppliedInTemplate()
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
        actual.Should().Be(expectedTemplate);
    }
}
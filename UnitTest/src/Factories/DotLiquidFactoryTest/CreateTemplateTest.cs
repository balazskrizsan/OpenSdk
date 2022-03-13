using DotLiquid;
using NUnit.Framework;
using OpenSdk.Factories;

namespace OpenSdk.UnitTest.Factories.DotLiquidFactoryTest;

public class CreateTemplateTest
{
    [Test]
    public void callsMethodWithTemplateString_returnsWithTemplateObject()
    {
        // Arrange
        var testedTemplate = "temp {{late}}";

        // Act
        var actual = new DotLiquidFactory().CreateTemplate(testedTemplate);

        // Assert
        Assert.IsInstanceOf<Template>(actual);
    }
}
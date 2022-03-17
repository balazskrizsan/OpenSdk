using DotLiquid;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSdk.Factories;

namespace OpenSdk.UnitTest.Factories.DotLiquidFactoryTest;

[TestClass]
public class CreateTemplateTest
{
    [TestMethod]
    public void callsMethodWithTemplateString_returnsWithTemplateObject()
    {
        // Arrange
        var testedTemplate = "temp {{late}}";

        // Act
        var actual = new DotLiquidFactory().CreateTemplate(testedTemplate);

        // Assert
        actual.Should().BeOfType<Template>();
    }
}
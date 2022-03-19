using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSdk.Services.GeneratorServices;
using OpenSdk.UnitTest.TestHelpers.FakeBuilders;
using OpenSdk.UnitTest.TestHelpers.MockBuilders;

namespace OpenSdk.UnitTest.Services.GeneratorServicesTest.InterfaceGeneratorServiceTest;

[TestClass]
public class GenerateFilesTest
{
    [TestMethod]
    public void GenerateOneInterfaceAndReturnVersion_Perfect()
    {
        // Arrange
        var templateServiceMock = TemplateServiceMockBuilder.CreateMock();
        var loggerMock = LoggerMockBuilder<InterfaceGeneratorService>.CreateMock();
        var testedMethodList = new MethodFakeBuilder().GetAsList();
        var expectedFiles = new FileFakeBuilder().BuildBothAsList();
        
        // Act
        var actual = new InterfaceGeneratorService(templateServiceMock, loggerMock)
            .GetGenerateFiles(testedMethodList);

        // Assert
        actual.Should().BeEquivalentTo(expectedFiles);
    }
}
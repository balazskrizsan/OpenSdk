using NUnit.Framework;
using OpenSdk.Services.GeneratorServices;
using OpenSdk.UnitTest.TestHelpers.FakeBuilders;
using OpenSdk.UnitTest.TestHelpers.MockBuilders;

namespace OpenSdk.UnitTest.Services.GeneratorServicesTest.InterfaceGeneratorServiceTest;

public class GenerateFilesTest
{
    [Test]
    public void GenerateOneInterfaceAndReturnVersion_Perfect()
    {
        // Arrange
        var templateServiceMock = TemplateServiceMockBuilder.GetMock();
        var loggerMock = LoggerMockBuilder.GetMock();
        var testedMethodList = new MethodFakeBuilder().GetAsList();
        var expectedFiles = new FileFakeBuilder().BuildBothAsList();
        
        // Act
        var actual = new InterfaceGeneratorService(templateServiceMock.Object, loggerMock.Object)
            .GenerateFiles(testedMethodList);

        // Assert
        Assert.AreEqual(expectedFiles, actual);
    }
}
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using OpenSdk.Services;
using OpenSdk.UnitTest.TestHelpers.FakeBuilders;
using OpenSdk.UnitTest.TestHelpers.MockBuilders;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.UnitTest.Services.GeneratorServiceTest;

[TestClass]
public class GenerateTest
{
    [TestMethod]
    public void collectFilesFromGeneratorMocks_callsTheFileServiceSaveFile()
    {
        // Arrange
        var testedParserResponse = new ParserResponseFakeBuilder().Build();

        var loggerMock = LoggerMockBuilder<ParserService>.CreateMock();
        var interfaceGeneratorServiceMock = InterfaceGeneratorServiceMockBuilder.CreateMock();
        var valueObjectGeneratorServiceMock = ValueObjectGeneratorServiceMockBuilder.CreateMock();
        var fileServiceMock = FileServiceMockBuilder.CreateMock();

        interfaceGeneratorServiceMock
            .GetGenerateFiles(Arg.Is<List<Method>>(testedParserResponse.Methods))
            .Returns(new FileFakeBuilder().BuildBothAsList());
        valueObjectGeneratorServiceMock
            .GetGeneratedFiles(Arg.Is<List<Schema>>(testedParserResponse.Schemas))
            .Returns(new FileFakeBuilder().BuildAsList());

        var expectedFileList = new FileFakeBuilder().BuildBothAsList();
        expectedFileList.AddRange(new FileFakeBuilder().BuildAsList());

        // Act
        new GeneratorService(
                loggerMock,
                interfaceGeneratorServiceMock,
                valueObjectGeneratorServiceMock,
                fileServiceMock
            )
            .Generate(testedParserResponse);

        // Assert
        fileServiceMock.Received()
            .SaveFilesAsync(Arg.Is<List<File>>(
                list => new CompareLogic().Compare(list, expectedFileList).AreEqual
            ));
    }
}
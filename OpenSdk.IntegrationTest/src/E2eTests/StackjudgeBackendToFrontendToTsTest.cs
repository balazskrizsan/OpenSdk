using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static OpenSdk.IntegrationTest.Extensions.AssertionExtensions;

namespace OpenSdk.IntegrationTest.E2eTests;

[TestClass]
public class StackjudgeBackendToFrontendToTs : AbstractIntegrationTest
{
    private static readonly string TestOutputFolder = $"{INTEGRATION_ROOT_FOLDER}\\TestFullAppOutput";

    [TestInitialize()]
    public void Startup() => ClearTestFolder();

    [TestCleanup()]
    public void Cleanup() => ClearTestFolder();

    private void ClearTestFolder()
    {
        try
        {
            // new DirectoryInfo(TestOutputFolder).Delete(true);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    [TestMethod]
    public void RunTheApplicationWithStackjudgeBackendToFrontendJson_GeneratesEverythingWellToTs()
    {
        // Arrange
        var inputFilePath = $"{INTEGRATION_ROOT_FOLDER}\\TestOpenapiJsons\\test_2_stackjudge_backend_to_frontend.json";
        var namespaceValue = "\\src";

        var testOpenapiGeneratedFilesForDiff = $"{INTEGRATION_ROOT_FOLDER}\\TestOpenapiGeneratedFilesForDiff\\test_2";
        var interfacesForDiffPath =
            $"{testOpenapiGeneratedFilesForDiff}{namespaceValue}\\stackjudge-frontend-sdk\\schema_interfaces";
        var parameterObjectsForDiffPath =
            $"{testOpenapiGeneratedFilesForDiff}{namespaceValue}\\stackjudge-frontend-sdk\\schema_parameter_objects";

        var testFullAppOutput = TestOutputFolder;
        var interfacesPath = $"{testFullAppOutput}{namespaceValue}\\stackjudge-frontend-sdk\\schema_interfaces";
        var parameterObjectsPath = $"{testFullAppOutput}{namespaceValue}\\stackjudge-frontend-sdk\\schema_parameter_objects";

        var services = GetServices(APP_ARGS_PRESET_TYPESCRITP(inputFilePath, TestOutputFolder + namespaceValue));

        var expectedInterfaceFilesForCompare = new List<string>
        {
            "IAccountGetByReviewIdByReviewId.ts",
        };
        var expectedParameterObjectFilesForCompare = new List<string>
        {
            "GetByReviewIdRequest.ts",
            "ResponseDataUser.ts",
            "User.ts",
        };
        var expectedFileNumber = expectedInterfaceFilesForCompare.Count + expectedParameterObjectFilesForCompare.Count;

        // Act
        services.GetService<ICliBootstrap>().Start();

        // Assert
        Directory
            .GetFiles(TestOutputFolder, "*.*", SearchOption.AllDirectories)
            .Length
            .Should()
            .Be(expectedFileNumber);

        foreach (var expectedFile in expectedInterfaceFilesForCompare)
        {
            var testedFileWithPath = $"{interfacesPath}\\{expectedFile}";
            var expectedFileWithPath = $"{interfacesForDiffPath}\\{expectedFile}";

            File(testedFileWithPath).Should().SameAs(expectedFileWithPath);
        }

        foreach (var expectedFile in expectedParameterObjectFilesForCompare)
        {
            var testedFileWithPath = $"{parameterObjectsPath}\\{expectedFile}";
            var expectedFileWithPath = $"{parameterObjectsForDiffPath}\\{expectedFile}";

            File(testedFileWithPath).Should().SameAs(expectedFileWithPath);
        }
    }
}
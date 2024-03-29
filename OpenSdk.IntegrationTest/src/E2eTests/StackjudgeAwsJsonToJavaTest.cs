using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static OpenSdk.IntegrationTest.Extensions.AssertionExtensions;

namespace OpenSdk.IntegrationTest.E2eTests;

[TestClass]
public class StackjudgeAwsJsonToJavaTest : AbstractIntegrationTest
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
            new DirectoryInfo(TestOutputFolder).Delete(true);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    [TestMethod]
    public void RunTheApplicationWithStackjudgeAwsJson_GeneratesEverythingWellToJava()
    {
        // Arrange
        var inputFilePath = $"{INTEGRATION_ROOT_FOLDER}\\TestOpenapiJsons\\test_1_stackjudge_aws.json";
        var namespaceValue = "\\com\\kbalazsworks";

        var testOpenapiGeneratedFilesForDiff = $"{INTEGRATION_ROOT_FOLDER}\\TestOpenapiGeneratedFilesForDiff\\test_1";
        var interfacesForDiffPath =
            $"{testOpenapiGeneratedFilesForDiff}{namespaceValue}\\stackjudge_aws_sdk\\schema_interfaces";
        var parameterObjectsForDiffPath =
            $"{testOpenapiGeneratedFilesForDiff}{namespaceValue}\\stackjudge_aws_sdk\\schema_parameter_objects";

        var testFullAppOutput = TestOutputFolder;
        var interfacesPath = $"{testFullAppOutput}{namespaceValue}\\stackjudge_aws_sdk\\schema_interfaces";
        var parameterObjectsPath = $"{testFullAppOutput}{namespaceValue}\\stackjudge_aws_sdk\\schema_parameter_objects";

        var services = GetServices(APP_ARGS_PRESET_JAVA(inputFilePath, TestOutputFolder + namespaceValue));

        var expectedInterfaceFilesForCompare = new List<string>
        {
            "IS3Upload.java",
            "ISesSendCompanyOwnEmail.java",
            "IV2S3Upload.java",
        };
        var expectedParameterObjectFilesForCompare = new List<string>
        {
            "ApiResponseDataCdnServicePutResponse.java",
            "ApiResponseDataListRemoteFile.java",
            "ApiResponseDataPutAndSaveResponse.java",
            "ApiResponseDataString.java",
            "CdnServicePutResponse.java",
            "GetS3UploadParams.java",
            "PostCompanyOwnEmailRequest.java",
            "PostUploadRequest.java",
            "PutAndSaveResponse.java",
            "RemoteFile.java",
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
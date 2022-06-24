using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static OpenSdk.IntegrationTest.Extensions.AssertionExtensions;

namespace OpenSdk.IntegrationTest;

[TestClass]
public class BootstrapStartTest : AbstractIntegrationTest
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
    public void RunTheApplicationWithValidYAML_GeneratesEverythingWell()
    {
        // Arrange
        var inputFilePath = $"{INTEGRATION_ROOT_FOLDER}\\TestOpenapiYamls\\test_1.json";
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

        var expectedFilesForCompare = GetExpectedFilesForCompare(interfacesPath, interfacesForDiffPath, parameterObjectsPath, parameterObjectsForDiffPath);

        // Act
        services.GetService<ICliBootstrap>().Start();

        // Assert
        Directory
            .GetFiles(TestOutputFolder, "*.*", SearchOption.AllDirectories)
            .Length
            .Should()
            .Be(expectedFilesForCompare.Count);
        
        foreach (var expectedFile in expectedFilesForCompare)
        {
            File(expectedFile.Key).Should().SameAs(expectedFile.Value);
        }
    }

    private static List<KeyValuePair<string, string>> GetExpectedFilesForCompare(string interfacesPath, string interfacesForDiffPath, string parameterObjectsPath, string parameterObjectsForDiffPath)
    {
        return new List<KeyValuePair<string, string>>
        {
            new(
                $"{interfacesPath}\\IS3Upload.java",
                $"{interfacesForDiffPath}\\IS3Upload.java"
            ),
            new(
                $"{interfacesPath}\\ISesSendCompanyOwnEmail.java",
                $"{interfacesForDiffPath}\\ISesSendCompanyOwnEmail.java"
            ),
            new(
                $"{parameterObjectsPath}\\ApiResponseDataCdnServicePutResponse.java",
                $"{parameterObjectsForDiffPath}\\ApiResponseDataCdnServicePutResponse.java"
            ),
            new(
                $"{parameterObjectsPath}\\CdnServicePutResponse.java",
                $"{parameterObjectsForDiffPath}\\CdnServicePutResponse.java"
            ),
            new(
                $"{parameterObjectsPath}\\PostCompanyOwnEmailRequest.java",
                $"{parameterObjectsForDiffPath}\\PostCompanyOwnEmailRequest.java"
            ),
            new(
                $"{parameterObjectsPath}\\PostUploadRequest.java",
                $"{parameterObjectsForDiffPath}\\PostUploadRequest.java"
            ),
        };
    }
}
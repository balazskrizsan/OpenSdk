using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenSdk.Registries;
using static OpenSdk.IntegrationTest.Extensions.AssertionExtensions;

namespace OpenSdk.IntegrationTest;

[TestClass]
public class BootstrapStartTest : AbstractIntegrationTest
{
    private static readonly string TestOutputFolder = $"{Environment.CurrentDirectory}\\TestFullAppOutput";
    
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
        var currentDirectory = Environment.CurrentDirectory;

        var inputFilePath = $"{currentDirectory}\\TestOpenapiYamls\\test_1.json";

        var testOpenapiGeneratedFilesForDiff = $"{currentDirectory}\\TestOpenapiGeneratedFilesForDiff\\test_1";
        var interfacesForDiffPath =
            $"{testOpenapiGeneratedFilesForDiff}\\com\\kbalazsworks\\stackjudge_aws_sdk\\schema_interfaces";
        var valueObjectsForDiffPath =
            $"{testOpenapiGeneratedFilesForDiff}\\com\\kbalazsworks\\stackjudge_aws_sdk\\schema_value_objects";

        var testFullAppOutput = TestOutputFolder;
        var interfacesPath = $"{testFullAppOutput}\\com\\kbalazsworks\\stackjudge_aws_sdk\\schema_interfaces";
        var valueObjectsPath = $"{testFullAppOutput}\\com\\kbalazsworks\\stackjudge_aws_sdk\\schema_value_objects";

        var services = GetServices(new ApplicationArgumentRegistry(inputFilePath, testFullAppOutput));

        var expectedFileCompersion = new List<KeyValuePair<string, string>>
        {
            new(
                $"{interfacesPath}\\IS3Upload.java",
                $"{interfacesForDiffPath}\\IS3Upload.java"
            ),
            new(
                $"{interfacesPath}\\IS3UploadWithReturn.java",
                $"{interfacesForDiffPath}\\IS3UploadWithReturn.java"
            ),
            new(
                $"{interfacesPath}\\ISesSendCompanyownemail.java",
                $"{interfacesForDiffPath}\\ISesSendCompanyownemail.java"
            ),
            new(
                $"{valueObjectsPath}\\ApiResponseDataCdnServicePutResponse.java",
                $"{valueObjectsForDiffPath}\\ApiResponseDataCdnServicePutResponse.java"
            ),
            new(
                $"{valueObjectsPath}\\CdnServicePutResponse.java",
                $"{valueObjectsForDiffPath}\\CdnServicePutResponse.java"
            ),
            new(
                $"{valueObjectsPath}\\PostCompanyOwnEmailRequest.java",
                $"{valueObjectsForDiffPath}\\PostCompanyOwnEmailRequest.java"
            ),
            new(
                $"{valueObjectsPath}\\PostUploadRequest.java",
                $"{valueObjectsForDiffPath}\\PostUploadRequest.java"
            ),
        };

        // Act
        services.GetService<ICliBootstrap>().Start();

        // Assert
        Directory
            .GetFiles(testFullAppOutput, "*.*", SearchOption.AllDirectories)
            .Length
            .Should()
            .Be(expectedFileCompersion.Count);

        foreach (var expectedFile in expectedFileCompersion)
        {
            File(expectedFile.Key).Should().SameAs(expectedFile.Value);
        }
    }
}
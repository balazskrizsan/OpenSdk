using System.Collections.Generic;
using OpenSdk.ValueObjects;

namespace OpenSdk.UnitTest.TestHelpers.FakeBuilders;

public class FileFakeBuilder
{
    private string DestinationFolder => "\\com\\kbalazsworks\\stackjudge_aws_sdk\\schema_interfaces";
    private string DestinationFolderWithResponse => "\\com\\kbalazsworks\\stackjudge_aws_sdk\\schema_interfaces";
    string FileName => "IS3Upload.java";
    string FileNameWithResponse => "IS3UploadWithReturn.java";
    string Content => "generated template string";
    string ContentWithResponse => "generated template string";

    public List<File> BuildAsList()
    {
        return new List<File>
        {
            new(DestinationFolder, FileName, Content)
        };
    }

    public List<File> BuildBothAsList()
    {
        return new List<File>
        {
            new(DestinationFolder, FileName, Content),
            new(DestinationFolderWithResponse, FileNameWithResponse, ContentWithResponse)
        };
    }
}
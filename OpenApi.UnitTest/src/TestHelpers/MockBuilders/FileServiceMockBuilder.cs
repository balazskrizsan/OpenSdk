using NSubstitute;
using OpenSdk.Services;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class FileServiceMockBuilder
{
    public static IFileService CreateMock() => Substitute.For<IFileService>();
}
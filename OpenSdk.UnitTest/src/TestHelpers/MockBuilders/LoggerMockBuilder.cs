using Microsoft.Extensions.Logging;
using NSubstitute;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class LoggerMockBuilder<T>
{
    public static ILogger<T> CreateMock() => Substitute.For<ILogger<T>>();
}
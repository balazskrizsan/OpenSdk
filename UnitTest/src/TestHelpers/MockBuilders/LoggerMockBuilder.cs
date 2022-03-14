using Microsoft.Extensions.Logging;
using Moq;
using OpenSdk.Services.GeneratorServices;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class LoggerMockBuilder
{
    public static Mock<ILogger<InterfaceGeneratorService>> GetMock()
    {
        return new Mock<ILogger<InterfaceGeneratorService>>();
    }
}
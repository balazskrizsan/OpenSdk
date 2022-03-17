using NSubstitute;
using OpenSdk.Services.GeneratorServices;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class InterfaceGeneratorServiceMockBuilder
{
    public static IInterfaceGeneratorService CreateMock() => Substitute.For<IInterfaceGeneratorService>();
}
using NSubstitute;
using OpenSdk.Registries;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class ApplicationArgumentRegistryMockBuilder
{
    public static IApplicationArgumentRegistry CreateMock() => Substitute.For<IApplicationArgumentRegistry>();
}
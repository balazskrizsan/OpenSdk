using NSubstitute;
using OpenSdk.Services.GeneratorServices;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class MapperServiceMockBuilder
{
    public static IMapperService CreateMock() => Substitute.For<IMapperService>();
}
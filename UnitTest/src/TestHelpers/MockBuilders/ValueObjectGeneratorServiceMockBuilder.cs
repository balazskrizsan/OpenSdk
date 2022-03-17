using NSubstitute;
using OpenSdk.Services.GeneratorServices;

namespace OpenSdk.UnitTest.TestHelpers.MockBuilders;

public static class ValueObjectGeneratorServiceMockBuilder
{
    public static IValueObjectGeneratorService CreateMock() => Substitute.For<IValueObjectGeneratorService>();
}
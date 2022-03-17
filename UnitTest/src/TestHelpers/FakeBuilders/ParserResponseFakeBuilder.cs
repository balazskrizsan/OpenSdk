using System.Collections.Generic;
using OpenSdk.ValueObjects;
using OpenSdk.ValueObjects.Generator;

namespace OpenSdk.UnitTest.TestHelpers.FakeBuilders;

public class ParserResponseFakeBuilder
{
    public List<Method> Methods { get; } = new MethodFakeBuilder().GetAsList();
    public List<Schema> Schemas { get; } = new SchemaFakeBuilder().BuildBothAsList();

    public ParserResponse Build() => new(Methods, Schemas);
}
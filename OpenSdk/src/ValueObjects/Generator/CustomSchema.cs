using System;
using System.Collections.Generic;

namespace OpenSdk.ValueObjects.Generator;

public class CustomSchema
{
    public String ClassName { get; }
    public Dictionary<string, Property>  Parameters { get; }

    public CustomSchema(string className, Dictionary<string, Property> parameters)
    {
        ClassName = className;
        Parameters = parameters;
    }
}
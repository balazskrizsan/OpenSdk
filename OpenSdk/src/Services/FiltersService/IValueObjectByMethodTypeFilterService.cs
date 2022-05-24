using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services.Filters;

public interface IValueObjectByMethodTypeFilterService
{
    Dictionary<string, List<string>> Get(Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> uris);
}
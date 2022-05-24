using System;
using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser;

namespace OpenSdk.Services.Filters;

public class ValueObjectByMethodTypeFilterService : IValueObjectByMethodTypeFilterService
{
    public Dictionary<string, List<string>> Get(
        Dictionary<string, Dictionary<string, PathUriMethodMethodDetails>> uris
    )
    {
        var valueObjectByMethodType = new Dictionary<string, List<string>>();

        foreach (var (uri, uriItem) in uris)
        {
            foreach (var (methodType, pathUriMethodMethodDetails) in uriItem)
            {
                foreach (var (contentName, content) in pathUriMethodMethodDetails.RequestBody)
                {
                    foreach (var (contentType, contentDetails) in content)
                    {
                        foreach (var (schemaName, contentDetail) in contentDetails)
                        {
                            foreach (var (schemaType, schemaValue) in contentDetail)
                            {
                                // @todo: if data is a ref, load the sub value object
                                if (schemaType == "$ref")
                                {
                                    if (!valueObjectByMethodType.ContainsKey(schemaValue))
                                    {
                                        valueObjectByMethodType.Add(schemaValue, new List<string> { methodType });

                                        continue;
                                    }

                                    valueObjectByMethodType.TryGetValue(schemaValue, out var relatedMethods);
                                    if (null != relatedMethods)
                                    {
                                        relatedMethods.Add(methodType);
                                        valueObjectByMethodType.TryAdd(schemaValue, relatedMethods);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return valueObjectByMethodType;
    }
}
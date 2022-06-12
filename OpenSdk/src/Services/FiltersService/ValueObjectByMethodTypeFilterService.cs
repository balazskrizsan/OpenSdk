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
                if (methodType == "post")
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
                if (methodType == "get")
                {
                    foreach (var parameter in pathUriMethodMethodDetails.Parameters)
                    {
                        // @todo: remove duplication
                        foreach (var (schemaType, schemaValue) in parameter.Schema)
                        {
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

        return valueObjectByMethodType;
    }
}
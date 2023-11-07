using System;
using System.Collections.Generic;
using OpenSdk.ValueObjects.Parser.Parser;

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
                            foreach (var schemaItem in contentDetails.Values)
                            {
                                if (schemaItem.Ref is not null)
                                {
                                    var paramObjectName = GetParamObjectNameFromRef(schemaItem.Ref);

                                    if (!valueObjectByMethodType.ContainsKey(paramObjectName))
                                    {
                                        valueObjectByMethodType.Add(paramObjectName, new List<string> { methodType });

                                        continue;
                                    }

                                    valueObjectByMethodType.TryGetValue(paramObjectName, out var relatedMethods);
                                    if (null != relatedMethods)
                                    {
                                        relatedMethods.Add(methodType);
                                        valueObjectByMethodType.TryAdd(paramObjectName, relatedMethods);
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
                        if (parameter.Schema.Ref is not null)
                        {
                            if (!valueObjectByMethodType.ContainsKey(parameter.Schema.Ref))
                            {
                                valueObjectByMethodType.Add(parameter.Schema.Ref, new List<string> { methodType });

                                continue;
                            }

                            valueObjectByMethodType.TryGetValue(parameter.Schema.Ref, out var relatedMethods);
                            if (null != relatedMethods)
                            {
                                relatedMethods.Add(methodType);
                                valueObjectByMethodType.TryAdd(parameter.Schema.Ref, relatedMethods);
                            }
                        }
                    }
                }
            }
        }

        return valueObjectByMethodType;
    }

    private string GetParamObjectNameFromRef(string value)
    {
        try
        {
            return value.Split("/")[3];
        }
        catch (Exception e)
        {
            throw new Exception("Reference parse error: " + value);
        }
    }

}
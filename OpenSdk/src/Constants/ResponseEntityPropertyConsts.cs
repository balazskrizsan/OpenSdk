using System.Collections.Generic;

namespace OpenSdk.Constants;

public static class ResponseEntityPropertyConsts
{
    public const string SUCCESS = "success";
    public const string ERROR_CODE = "errorCode";
    public const string REQUEST_ID = "requestId";
    public const string DATA = "data";

    public static IList<string> AsListWithoutData()
    {
        return new[] { SUCCESS, ERROR_CODE, REQUEST_ID };
    }
}
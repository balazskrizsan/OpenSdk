using System.Collections.Generic;

namespace OpenSdk.Constaints;

public class ResponseEntityPropertyConst
{
    public const string SUCCESS = "success";
    public const string ERROR_CODE = "errorCode";
    public const string REQUEST_ID = "requestId";
    public const string DATA = "data";

    public static IList<string> asListWithoutData()
    {
        return new[]
        {
            SUCCESS,
            ERROR_CODE,
            REQUEST_ID
        };
    }
}
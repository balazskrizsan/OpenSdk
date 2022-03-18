package com.kbalazsworks.stackjudge_aws_sdk.schema_interfaces;

import com.kbalazsworks.stackjudge_aws_sdk.common.exceptions.ResponseException;
import com.kbalazsworks.stackjudge_aws_sdk.common.entities.StdResponse;
import com.kbalazsworks.stackjudge_aws_sdk.common.interfaces.IOpenSdkPostable;
import com.kbalazsworks.stackjudge_aws_sdk.schema_parameter_objects.*;

import javax.annotation.processing.Generated;

@Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
public interface IS3UploadWithReturn
{
    default String getApiUri()
    {
        return "/s3/upload";
    }

    StdResponse<CdnServicePutResponse> execute(IOpenSdkPostable postUploadRequest)
    throws ResponseException;
}

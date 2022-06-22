package stackjudge_aws_sdk.schema_interfaces;

import com.kbalazsworks.stackjudge_aws_sdk.common.entities.StdResponse;
import com.kbalazsworks.stackjudge_aws_sdk.common.exceptions.ResponseException;
import com.kbalazsworks.stackjudge_aws_sdk.common.interfaces.*;
import com.kbalazsworks.stackjudge_aws_sdk.schema_parameter_objects.*;
import org.springframework.scheduling.annotation.Async;

import javax.annotation.processing.Generated;
import java.util.concurrent.Future;

@Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
public interface ISesSendCompanyOwnEmail
{
    default String getApiUri()
    {
        return "/ses/send/company-own-email";
    }

    void post(IOpenSdkPostable postCompanyOwnEmailRequest)
    throws ResponseException;

    @Async
    void postAsync(IOpenSdkPostable postCompanyOwnEmailRequest)
    throws ResponseException;
}

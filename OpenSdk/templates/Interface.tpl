package {{Namespace}};

import com.kbalazsworks.stackjudge_aws_sdk.common.entities.OpenSdkStdResponse;
import com.kbalazsworks.stackjudge_aws_sdk.common.interfaces.IOpenSdkPostable;
import com.kbalazsworks.stackjudge_aws_sdk.schema_parameter_objects.*;

import javax.annotation.processing.Generated;

@Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
public interface {{InterfaceName}}
{
    default String getApiUri()
    {
        return "{{MethodUri}}";
    }

    {{ExecReturnType}} execute(IOpenSdkPostable {{ParamObjectVarName}});
}

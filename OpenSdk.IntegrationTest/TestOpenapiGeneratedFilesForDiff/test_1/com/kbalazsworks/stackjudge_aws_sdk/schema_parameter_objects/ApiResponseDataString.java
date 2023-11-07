package stackjudge_aws_sdk.schema_parameter_objects;

import com.fasterxml.jackson.annotation.JsonProperty;
import stackjudge_aws_sdk.common.interfaces.*;
import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.extern.jackson.Jacksonized;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.http.HttpEntity;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;

import javax.annotation.processing.Generated;
import java.util.*;

@Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
@Jacksonized
@Builder(access = AccessLevel.PUBLIC)
@AllArgsConstructor
@Getter
public final class ApiResponseDataString
{
    @JsonProperty("data")
    private final String data;
    @JsonProperty("success")
    private final Boolean success;
    @JsonProperty("errorCode")
    private final Integer errorCode;
    @JsonProperty("requestId")
    private final String requestId;
}

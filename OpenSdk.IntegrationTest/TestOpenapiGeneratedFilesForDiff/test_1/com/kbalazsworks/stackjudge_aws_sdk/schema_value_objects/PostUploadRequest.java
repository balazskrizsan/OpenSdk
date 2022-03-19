package com.kbalazsworks.stackjudge_aws_sdk.schema_value_objects;

import com.fasterxml.jackson.annotation.JsonProperty;
import com.kbalazsworks.stackjudge_aws_sdk.common.interfaces.IOpenSdkPostable;
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
import java.util.List;

@Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
@Jacksonized
@Builder(access = AccessLevel.PUBLIC)
@AllArgsConstructor
@Getter
public final class PostUploadRequest implements IOpenSdkPostable
{
    @JsonProperty("cdnNamespace")
    private final String cdnNamespace;
    @JsonProperty("subFolder")
    private final String subFolder;
    @JsonProperty("fileName")
    private final String fileName;
    @JsonProperty("fileExtension")
    private final String fileExtension;
    @JsonProperty("#/components/schemas/FileUpload")
    private final HttpEntity<ByteArrayResource> content;

    @Override
    public MultiValueMap<String, Object> toOpenSdkPost()
    {
        return new LinkedMultiValueMap<>()
        {{
            addAll("cdnNamespace", List.of(cdnNamespace));
            addAll("subFolder", List.of(subFolder));
            addAll("fileName", List.of(fileName));
            addAll("fileExtension", List.of(fileExtension));
            addAll("content", List.of(content));
        }};
    }
}

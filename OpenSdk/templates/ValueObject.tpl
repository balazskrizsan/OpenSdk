package {{NamespaceValue}};

import com.kbalazsworks.stackjudge_aws_sdk.common.interfaces.IOpenSdkPostable;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.http.HttpEntity;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;

import javax.annotation.processing.Generated;
import java.util.List;

@Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
public final class {{ValueObjectName}} implements IOpenSdkPostable
{
    {%- for parameter in Parameters -%}
    private final {{parameter.Key}} {{parameter.Value}};
    {%- endfor -%}

    public {{valueObjectName}}(
    {%- for parameter in parameters -%}
        {{parameter.Key}} {{parameter.Value}}{%- if parameter != parameters.last -%},{%- endif %}
    {%- endfor -%}
    )
    {
    {%- for parameter in parameters -%}
        this.{{parameter.Value}} = {{parameter.Value}};
    {%- endfor -%}
    }
    {%- for parameter in parameters %}
    public {{parameter.Key}} {{parameter.Value}}()
    {
        return {{parameter.Value}};
    }
    {%- endfor -%}

    @Override
    public MultiValueMap<String, Object> toOpenSdkPost()
    {
        return new LinkedMultiValueMap<>()
        {{ "{{" }}
        {%- for parameter in parameters -%}
            addAll("{{parameter.Value}}", List.of({{parameter.Value}}()));
        {%- endfor -%}
        {{ "}}" }};
    }

    // not yet supported in the generator
//    @Override
//    public boolean equals(Object obj)
//    {
//    }

    // not yet supported in the generator
//    @Override
//    public int hashCode()
//    {
//    }

    // not yet supported in the generator
//    @Override
//    public String toString()
//    {
//    }
}

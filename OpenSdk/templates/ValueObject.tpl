package {{namespaceValue}};

import com.kbalazsworks.stackjudge_aws_sdk.common.interfaces.IOpenSdkPostable;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.http.HttpEntity;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;

import javax.annotation.processing.Generated;
import java.util.List;

@Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
public final class {{valueObjectName}} implements IOpenSdkPostable
{
    {{for name, type in parameters:

    private final {{type}} {{name}};
    }}


    {{ set i to 1 }}

    public {{valueObjectName}}(

    {{for name, type in parameters:

            {{type}} {{name}}{{if len(parameters) > i:
            ,
            }}


            {{set i to i + 1)}}
    }}
    {{ set i to 1 }}
    )
    {
        {{for name, type in parameters:
    
        this.{{name}} = {{name}};
        }}
    
    }

    {{for name, type in parameters:
    
    public {{type}} {{name}}()
    {
        return {{name}};
    }

    }}

    @Override
    public MultiValueMap<String, Object> toOpenSdkPost()
    {
        return new LinkedMultiValueMap<>()
        \{{
            {{for name, type in parameters:
        
            addAll("{{name}}", List.of({{name}}()));
            }}

        \}};
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



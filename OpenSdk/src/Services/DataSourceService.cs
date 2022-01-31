namespace OpenSdk.Services
{
    public class DataSourceService
    {
        public string GetYaml()
        {
            return @"{
              ""openapi"" : ""3.0.3"",
              ""info"" : {
                ""title"" : ""stackjudge-aws API"",
                ""version"" : ""1.0-SNAPSHOT""
              },
              ""paths"" : {
                ""/ses/send/company-own-email"" : {
                  ""post"" : {
                    ""tags"" : [ ""Post Send Action"" ],
                    ""requestBody"" : {
                      ""content"" :  {
                        ""multipart/form-data"": {
                          ""schema"": {
                             ""$ref"" : ""#/components/schemas/PostCompanyOwnEmailRequest""
                           }
                        }
                      }
                    },
                    ""responses"" : {
                      ""201"" : {
                        ""description"" : ""Created""
                      }
                    }
                  }
                }
              },
              ""components"" : {
                ""schemas"": {
                  ""PostCompanyOwnEmailRequest"" : {
                    ""type"" : ""object"",
                    ""properties"" : {
                      ""to"" : {
                        ""type"" : ""string""
                      },
                      ""varName"" : {
                        ""type"" : ""lofasz""
                      },
                    }
                  }
                }
              }
            }";
        }
    }
}

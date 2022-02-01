using System;
using System.IO;

namespace OpenSdk.Services
{
    public class DataSourceService
    {
        private string openApiFilePath;

        public DataSourceService(string openApiFilePath)
        {
          this.openApiFilePath = openApiFilePath;
        }

        public string Get()
        {
            return File.ReadAllText(openApiFilePath);
        }
    }
}

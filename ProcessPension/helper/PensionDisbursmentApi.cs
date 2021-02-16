using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessPension.helper
{
    public class PensionDisbursmentApi
    {
        public  static HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/");
            return client;
        }
    }
}

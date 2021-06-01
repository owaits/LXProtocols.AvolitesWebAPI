using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LXProtocols.AvolitesWebAPI
{
    public class Fixtures
    {
        private HttpClient http = null;

        internal Fixtures(HttpClient http)
        {
            this.http = http;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    public class CueLists
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CueLists"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal CueLists(HttpClient http)
        {
            this.http = http;
        }

        public async Task<float> LiveCueNumber()
        {
            return await http.GetFromJsonAsync<float>($"titan/get/2/CueLists/LiveCueNumber");
        }
    }
}

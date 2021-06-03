using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Implements WebAPI methods related to controlling fixtures.
    /// </summary>
    public class Fixtures
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fixtures"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Fixtures(HttpClient http)
        {
            this.http = http;
        }
    }
}

using System;
using System.Net;
using System.Net.Http;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Establishes an HTTP connection to the Titan console and opens the API  interface.
    /// </summary>
    /// <remarks>
    /// Create this class to open the API connection and call any API function that has been implemented.
    /// 
    /// Use the console IP address or name, for remote connections use ports 4430 or 4530 and for local connections use ports 4431 and 4531.
    /// </remarks>
    public class Titan:IDisposable
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Titan"/> class.
        /// </summary>
        /// <param name="consoleAddress">The console IP address used to connect to WebAPI.</param>
        public Titan(IPAddress consoleAddress) : this(consoleAddress.ToString(), 4430)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Titan"/> class.
        /// </summary>
        /// <param name="consoleAddress">The console IP address used to connect to WebAPI.</param>
        /// <param name="port">The port used for WebAPI.</param>
        /// <param name="https">Whether to use SSL for the HTTP requests.</param>
        public Titan(IPAddress consoleAddress, int port):this(consoleAddress.ToString(), port)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Titan"/> class.
        /// </summary>
        /// <param name="consoleAddress">The console IP address or name used to connect to WebAPI.</param>
        public Titan(string consoleAddress):this(consoleAddress, 4531)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Titan"/> class.
        /// </summary>
        /// <param name="consoleAddress">The console IP address or name used to connect to WebAPI.</param>
        /// <param name="port">The port used for WebAPI.</param>
        /// <param name="https">Whether to use SSL for the HTTP requests.</param>
        public Titan(string consoleAddress, int port, bool https=false)
        {
            string protocol = https ? "https" : "http";
            http = new HttpClient() { BaseAddress = new Uri($"{protocol}://{consoleAddress}:{port}") };
            Playbacks = new Playbacks(http);
            Fixtures = new Fixtures(http);
        }

        /// <summary>
        /// Gets all the API functions relating to playbacks.
        /// </summary>
        public Playbacks Playbacks { get; private set; }

        /// <summary>
        /// Gets all the API functions relating to fixtures.
        /// </summary>
        public Fixtures Fixtures { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if(http != null)
            {
                http.Dispose();
                http = null;
            }            
        }
    }
}

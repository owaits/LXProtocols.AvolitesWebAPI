using LXProtocols.AvolitesWebAPI.Information;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
            Handles = new Handles(http);
            Menu = new Menu(http);
            Playbacks = new Playbacks(http);
            CueLists = new CueLists(http);
            Fixtures = new Fixtures(http);
            Palettes = new Palettes(http);
            Macros = new Macros(http);
        }

        /// <summary>
        /// Gets information about the device we are connected to through WebAPI.
        /// </summary>
        /// <remarks>
        /// If you need to update the device information call IsConnected().
        /// </remarks>
        public DeviceInformation ConnectedDevice { get; set; }

        /// <summary>
        /// Gets all the API functions relating to handles.
        /// </summary>
        public Handles Handles { get; private set; }

        /// <summary>
        /// The menu API controls much of the behaviour of the console, use this to control it.
        /// </summary>
        public Menu Menu { get; set; }

        /// <summary>
        /// Gets all the API functions relating to playbacks.
        /// </summary>
        public Playbacks Playbacks { get; private set; }

        public CueLists CueLists { get; private set; }

        /// <summary>
        /// Gets all the API methods relating to fixtures.
        /// </summary>
        public Fixtures Fixtures { get; private set; }

        /// <summary>
        /// Gets all the API methods relating to palettes.
        /// </summary>
        public Palettes Palettes { get; private set; }

        /// <summary>
        /// The macros API allows you to download, upload and run macros. 
        /// </summary>
        public Macros Macros { get; private set; }

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

        /// <summary>
        /// Determines if we have a valid WEbAPI connection to a Titan console.
        /// </summary>
        /// <returns>True if the connection is valid and we can see a WebAPI endpoint.</returns>
        public async Task<bool> IsConnected()
        {
            try
            {
                var response = await http.GetAsync("titan/get/2/Titan/DeviceInfo");

                if(response.IsSuccessStatusCode)
                {
                    ConnectedDevice = await response.Content.ReadFromJsonAsync<DeviceInformation>();
                    return true;
                }
                return false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}

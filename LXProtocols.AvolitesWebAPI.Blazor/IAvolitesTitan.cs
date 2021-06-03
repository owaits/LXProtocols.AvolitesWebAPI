using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    /// <summary>
    /// Provides access to the Titan WebAPI and allows you to call API methods.
    /// </summary>
    public interface IAvolitesTitan
    {
        /// <summary>
        /// Gets or sets a value indicating whether HTTPS should be used for connections to WebAPI. The default is HTTP with no transport security.
        /// </summary>
        public bool HttpsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the console IP address or name to use to connect to the console.
        /// </summary>
        public string ConsoleAddress { get; set; }

        /// <summary>
        /// Gets or sets the HTTP port to use when connection to the console, the default is 4430.
        /// </summary>
        /// <remarks>
        /// If you are using a none standard port for WebAPI you can override the default.
        /// </remarks>
        public int ConsolePort { get; set; }

        /// <summary>
        /// Gets the API root from which you may call functions for a variety of API functions.
        /// </summary>
        public Titan API { get; }
    }
}

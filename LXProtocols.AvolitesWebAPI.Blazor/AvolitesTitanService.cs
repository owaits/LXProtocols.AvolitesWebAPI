using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    /// <summary>
    /// This is a blazor service that can be injected into components and centrally manages the connection to the Avolites WebAPI.
    /// </summary>
    /// <seealso cref="LXProtocols.AvolitesWebAPI.Blazor.IAvolitesTitan" />
    public class AvolitesTitanService : IAvolitesTitan
    {
        private Titan titan = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvolitesTitanService"/> class.
        /// </summary>
        public AvolitesTitanService()
        {
            
        }

        /// <summary>
        /// Gets or sets a value indicating whether HTTPS should be used for connections to WebAPI. The default is HTTP with no transport security.
        /// </summary>
        public bool HttpsEnabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the console IP address or name to use to connect to the console.
        /// </summary>
        public string ConsoleAddress { get; private set; } = "localhost";

        /// <summary>
        /// Gets or sets the HTTP port to use when connection to the console, the default is 4430.
        /// </summary>
        /// <remarks>
        /// If you are using a none standard port for WebAPI you can override the default.
        /// </remarks>
        public int ConsolePort { get; private set; } = 4530;

        /// <summary>
        /// Gets the API root from which you may call functions for a variety of API functions.
        /// </summary>
        public Titan API 
        { 
            get 
            {
                if (titan == null)
                    throw new InvalidOperationException("No connection to Titan has been established. You must call connect with the correct address first.");
                return titan; 
            } 
        }

        /// <summary>
        /// Makes a connection to the Titan console with the specified address.
        /// </summary>
        /// <param name="address">The address of the console to connect to.</param>
        /// <param name="port">The port to use when connecting.</param>
        public void Connect(string address, int port)
        {
            ConsoleAddress = address;
            ConsolePort = port;

            if(titan != null)
            {
                titan.Dispose();
            }

            this.titan = new Titan(ConsoleAddress, ConsolePort, HttpsEnabled);
        }

        /// <summary>
        /// Whether a valid Titan connection exists.
        /// </summary>
        public bool IsConnected()
        {
            return this.titan != null;
        }
    }

    /// <summary>
    /// Provides Extensions to help using AvolitesTitanService within a blazor application.
    /// </summary>
    public static class AvolitesTitanServiceExtensions
    {
        /// <summary>
        /// Builds the Avolites Titan WebAPI service and allows components to inject the IAvolitesTitan interface and use this to call WebAPI methods.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddAvolitesTitan(this IServiceCollection services)
        {
            return services.AddScoped<IAvolitesTitan,AvolitesTitanService>();
        }

    }
}

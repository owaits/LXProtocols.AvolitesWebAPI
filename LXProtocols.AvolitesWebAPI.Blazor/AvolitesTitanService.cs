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
            this.titan = new Titan(ConsoleAddress, ConsolePort, HttpsEnabled);
        }

        /// <summary>
        /// Gets or sets a value indicating whether HTTPS should be used for connections to WebAPI. The default is HTTP with no transport security.
        /// </summary>
        public bool HttpsEnabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the console IP address or name to use to connect to the console.
        /// </summary>
        public string ConsoleAddress { get; set; } = "localhost";

        /// <summary>
        /// Gets or sets the HTTP port to use when connection to the console, the default is 4430.
        /// </summary>
        /// <remarks>
        /// If you are using a none standard port for WebAPI you can override the default.
        /// </remarks>
        public int ConsolePort { get; set; } = 4530;

        /// <summary>
        /// Gets the API root from which you may call functions for a variety of API functions.
        /// </summary>
        public Titan API { get { return titan; } }
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

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Implements WebAPI methods related to controlling playbacks.
    /// </summary>
    public class Palettes
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Palettes"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Palettes(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Applies a palette to the current fixture selection or if not fixtures are selected all fixtures.
        /// </summary>
        /// <param name="handle">The handle for the palette you want to apply.</param>
        /// <param name="withTimes">Whether the palette should snap or run with the current palette fade times.</param>
        /// <returns></returns>
        public async Task ApplyPalette(HandleReference handle, bool withTimes = true)
        {
            await http.GetAsync($"titan/script/2/Palette/ApplyPalette?{handle.ToQueryArgument("handle")}&usePaletteTimes={withTimes}");           
        }
    }
}

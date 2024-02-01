using LXProtocols.AvolitesWebAPI.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Implements WebAPI methods related to the DMX output.
    /// </summary>
    public class Dmx
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Playbacks"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Dmx(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Sets the streaming ACN merge priority.
        /// </summary>
        /// <param name="mergePriority">The merge priority where a higher priority will take presidence over a lower priority.</param>
        public async Task SetMergePriority(int mergePriority = 100)
        {            
            await http.GetAsync($"titan/script/2/Dmx/SetMergePriority?mergePriority={mergePriority}");
        }

        /// <summary>
        /// Allow the user to enable or disable DMX output.
        /// </summary>
        /// <param name="freeze">If set to true freeze the output.</param>
        public async Task FreezeDmx(bool freeze)
        {
            await http.GetAsync($"titan/script/2/Dmx/FreezeDmx?freeze={freeze}");
        }

    }
}

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
    public class Playbacks
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Playbacks"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Playbacks(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Fires the specified playback at full.
        /// </summary>
        /// <param name="userNumber">The user number of the playback to fire.</param>
        public async Task Fire(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?{handle.ToQueryArgument("handle")}&level=1&alwaysRefire=false");           
        }

        /// <summary>
        /// Fires the specified playback at the specified level..
        /// </summary>
        /// <param name="userNumber">The user number of the playback to fire.</param>
        /// <param name="level">The level to fire the playback at where 1 is full and 0 is off.</param>
        public async Task Level(HandleReference handle, float level)
        {            
            await http.GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?{handle.ToQueryArgument("handle")}&level={level}&alwaysRefire=false");
        }

        /// <summary>
        /// Kills the specified playback aithout releasing.
        /// </summary>
        /// <param name="userNumber">The user number of the playback to kill.</param>
        public async Task Kill(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/Playbacks/KillPlayback?{handle.ToQueryArgument("handle")}");
        }

        /// <summary>
        /// Kills all running playbacks without releasing.
        /// </summary>
        public async Task KillAll()
        {
            await http.GetAsync($"titan/script/2/Playbacks/KillAllPlaybacks");
        }
    }
}

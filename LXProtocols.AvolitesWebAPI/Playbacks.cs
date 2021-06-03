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
        /// Gets the handle information for a handle using the user number of the playback handle.
        /// </summary>
        /// <param name="userNumber">The playback user number to search for.</param>
        /// <returns>The handle information for the requested playback handle.</returns>
        public async Task<HandleInformation> GetHandleFromUserNumber(int userNumber)
        {
            return await http.GetFromJsonAsync<HandleInformation>($"titan/script/2/Handles/GetHandleFromUserNumber?handleXmlNodeName=playbackHandle&userNumber={userNumber}");
        }

        /// <summary>
        /// Fires the specified playback at full.
        /// </summary>
        /// <param name="userNumber">The user number of the playback to fire.</param>
        public async Task Fire(int userNumber)
        {
            await http .GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?handle_userNumber={userNumber}&level=1&alwaysRefire=false");
        }

        /// <summary>
        /// Fires the specified playback at the specified level..
        /// </summary>
        /// <param name="userNumber">The user number of the playback to fire.</param>
        /// <param name="level">The level to fire the playback at where 1 is full and 0 is off.</param>
        public async Task Level(int userNumber, float level)
        {
            await http.GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?handle_userNumber={userNumber}&level={level}&alwaysRefire=false");
        }

        /// <summary>
        /// Kills the specified playback aithout releasing.
        /// </summary>
        /// <param name="userNumber">The user number of the playback to kill.</param>
        public async Task Kill(int userNumber)
        {
            await http.GetAsync($"titan/script/2/Playbacks/KillPlayback?handle_userNumber={userNumber}");
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

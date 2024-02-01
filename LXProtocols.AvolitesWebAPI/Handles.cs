using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    public class Handles
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="HandleWorlds"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Handles(HttpClient http)
        {
            this.http = http;
        }

        #region Handles

        public async Task<HandleInformation[]> GetHandles(string handleGroupId = "", int pageIndex = -1, bool verbose = false)
        {
            if(!string.IsNullOrEmpty(handleGroupId))
            {
                if(pageIndex > 0)
                {
                    return await http.GetFromJsonAsync<HandleInformation[]>($"titan/handles/{handleGroupId}{(verbose? "?verbose=true":"")}");
                }
                else
                {
                    return await http.GetFromJsonAsync<HandleInformation[]>($"titan/handles/{handleGroupId}/{pageIndex}{(verbose ? "?verbose=true" : "")}");
                }
            }
            else
            {
                return await http.GetFromJsonAsync<HandleInformation[]>($"titan/handles{(verbose ? "?verbose=true" : "")}");
            }            
        }

        /// <summary>
        /// Gets the handle information for a handle using the user number of the playback handle.
        /// </summary>
        /// <param name="userNumber">The playback user number to search for.</param>
        /// <returns>The handle information for the requested playback handle.</returns>
        public async Task<HandleInformation> GetHandleFromUserNumber(int userNumber, string handleType = "playbackHandle")
        {
            return await http.GetFromJsonAsync<HandleInformation>($"titan/script/2/Handles/GetHandleFromUserNumber?handleXmlNodeName={handleType}&userNumber={userNumber}");
        }

        public async Task<HandleInformation> GetHandleFromTitanId(int titanId, string handleType = "playbackHandle")
        {
            return await http.GetFromJsonAsync<HandleInformation>($"titan/script/2/Handles/GetHandleFromId?handleXmlNodeName={handleType}&id={titanId}");
        }

        #endregion

        #region Handle Worlds

        public async Task SetHandleWorld(Guid worldId)
        {
            await http.GetAsync($"titan/script/2/Handles/SetHandleWorld?worldId={worldId}");
        }

        public async Task RenameHandleWorld(Guid worldId, string worldLegend)
        {
            await http.GetAsync($"titan/script/2/Handles/RenameHandleWorld?worldId={worldId}&worldLegend={worldLegend}");
        }

        public async Task<string> CurrentWorldId()
        {
            return await http.GetFromJsonAsync<string>($"titan/get/2/Handles/CurrentWorldId");
        }

        public async Task<string> CurrentWorldName()
        {
            return await http.GetFromJsonAsync<string>($"titan/get/2/Handles/CurrentWorldName");
        }

        #endregion
    }
}

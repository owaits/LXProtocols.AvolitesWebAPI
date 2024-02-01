using LXProtocols.AvolitesWebAPI.Information;
using LXProtocols.AvolitesWebAPI.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Implements WebAPI methods related to the Set Lists.
    /// </summary>
    public class SetList
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetList"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal SetList(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Gets the information for the specified set list.
        /// </summary>
        /// <param name="setListId">The ID of the set list to return information for.</param>
        /// <returns>The set list information.</returns>
        public async Task<SetListInformation> GetSetList(int setListId)
        {
            return (await http.GetFromJsonAsync<JsonInformation<SetListInformation>>($"titan/setlist/{setListId}")).Information;
        }

        /// <summary>
        /// Gets the set list track information for a track within a set list.
        /// </summary>
        /// <param name="setListId">The ID of the set list containing the track.</param>
        /// <param name="trackId">The ID of the track within the set list.</param>
        /// <returns>The set list track information.</returns>
        public async Task<SetListTrackInformation> GetSetListTrack(int setListId, int trackId)
        {
            return (await http.GetFromJsonAsync<JsonInformation<SetListTrackInformation>>($"titan/setListId/{setListId}/track/{trackId}")).Information;
        }

        /// <summary>
        /// Select the active set list.
        /// </summary>
        /// <param name="handle">The handle for the set list.</param>
        public async Task SelectList(HandleReference handle)
        {
            await http.GetAsync($"script/2/SetList/SelectListHandle?{handle.ToQueryArgument("setListHandle")}");
        }
        

        /// <summary>
        /// Gets the handle for the active set list track.
        /// </summary>
        /// <returns>The track handle for the active track.</returns>
        public async Task<HandleInformation> GetActiveTrack() 
        {
            return await http.GetFromJsonAsync<HandleInformation>($"titan/get/2/SetList/ActiveTrack");
        }

        /// <summary>
        /// Fires the next track in the active list.
        /// </summary>
        public async Task NextTrack()
        {
            await http.GetAsync($"titan/script/2/SetList/NextTrack");
        }

        /// <summary>
        /// Fires the previous track in the active list.
        /// </summary>
        public async Task PreviousTrack()
        {
            await http.GetAsync($"titan/script/2/SetList/PreviousTrack");
        }

        /// <summary>
        /// Fires the track.
        /// </summary>
        /// <param name="handle">The handle of the track to fire.</param>
        public async Task FireTrack(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/SetList/FireTrack?{handle.ToQueryArgument("trackHandle")}");
        }

    }
}

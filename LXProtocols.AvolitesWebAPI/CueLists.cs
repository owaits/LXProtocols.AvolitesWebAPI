using LXProtocols.AvolitesWebAPI.Information;
using LXProtocols.AvolitesWebAPI.JsonConverters;
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
    /// Avolites WebAPI functionality related to cue lists.
    /// </summary>
    public class CueLists
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CueLists"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal CueLists(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Gets the cue information given for a cue in a cue list using the titan ID.
        /// </summary>
        /// <param name="playbackId">The playback ID of the cue list containing the cue.</param>
        /// <param name="cueId">The ID of the cue to get information about.</param>
        /// <returns>The cue information for the specified cue.</returns>
        public async Task<CueInformation> GetCue(int playbackId, int cueId)
        {
            return (await http.GetFromJsonAsync<JsonInformation<CueInformation>>($"titan/playback/{playbackId}/cue/{cueId}")).Information;
        }

        /// <summary>
        /// Gets or sets the live cue number of the connected cueList.
        /// </summary>
        /// <returns>The live cue number of the connected cueList.</returns>
        public async Task<float> LiveCueNumber()
        {
            return await http.GetFromJsonAsync<float>($"titan/get/2/CueLists/LiveCueNumber");
        }

        /// <summary>
        /// Sets the next cue for the specified cue list.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        /// <param name="cueNumber">The cue number.</param>
        public async Task SetNextCue(HandleReference handle, float cueNumber)
        {
            await http.GetAsync($"titan/script/2/CueLists/SetNextCue?{handle.ToQueryArgument("handle")}&stepNumber={cueNumber}");
        }

        /// <summary>
        /// Plays the given playback. If it's paused it continues, if it's running it starts the next step
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        public async Task Play(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/CueLists/Play?{handle.ToQueryArgument("handle")}");
        }

        /// <summary>
        /// Plays the given playback but overides the fade time with the supplied value.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        /// <param name="fadeInTime">The fade in time.</param>
        public async Task PlayWithTime(HandleReference handle, TimeSpan fadeInTime)
        {
            await http.GetAsync($"titan/script/2/CueLists/PlayWithTime?{handle.ToQueryArgument("handle")}&time={fadeInTime.TotalSeconds}");
        }

        /// <summary>
        /// Pauses the given playback, or optionally, if the playback is already paused, goes back.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        /// <param name="goBackIfPaused">Whether to perform the go back / cancel link functionality instead if the playback is already paused.</param>
        public async Task Pause(HandleReference handle, bool goBackIfPaused = false)
        {
            await http.GetAsync($"titan/script/2/CueLists/Pause?{handle.ToQueryArgument("handle")}&goBackIfPaused={goBackIfPaused}");
        }

        /// <summary>
        /// Un-pauses a paused cue list if paused.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        /// <param name="time">An optional override time.</param>
        public async Task Resume(HandleReference handle, TimeSpan? time = null)
        {
            await http.GetAsync($"titan/script/2/CueLists/Resume?{handle.ToQueryArgument("handle")}&time={time?.TotalSeconds ?? 0}");
        }

        /// <summary>
        /// Reviews the live cue of the supplied playback. This snaps to the previous cue then runs the live cue again.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        public async Task Review(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/CueLists/Review?{handle.ToQueryArgument("handle")}");
        }

        /// <summary>
        /// Snaps back on the supplied playback. This snaps (fires without fades) to the cue previous to the live one.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        public async Task SnapBack(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/CueLists/SnapBack?{handle.ToQueryArgument("handle")}");
        }

        /// <summary>
        /// Plays back on the supplied playback. Fires the cue previous to the live one.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        public async Task GoBack(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/CueLists/GoBack?{handle.ToQueryArgument("handle")}");
        }

        /// <summary>
        /// Cuts the next step to live, that is runs it without fade times. Operates on the supplied playback.
        /// </summary>
        /// <param name="handle">The handle reference for the CueList.</param>
        public async Task CutNextCueToLive(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/CueLists/CutNextCueToLive?{handle.ToQueryArgument("handle")}");
        }

        /// <summary>
        /// Plays the next cue in the specified chase or cue list.
        /// <param name="handle">The handle reference for the CueList.</param>
        public async Task NextStep(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/CueLists/NextStep?{handle.ToQueryArgument("handle")}");
        }
    }
}

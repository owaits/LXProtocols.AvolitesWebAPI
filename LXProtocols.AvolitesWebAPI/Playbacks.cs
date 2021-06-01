using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    public class Playbacks
    {
        private HttpClient http = null;

        internal Playbacks(HttpClient http)
        {
            this.http = http;
        }

        public async Task<HandleInformation> GetHandleFromUserNumber(int userNumber)
        {
            return await http.GetFromJsonAsync<HandleInformation>($"titan/script/2/Handles/GetHandleFromUserNumber?handleXmlNodeName=playbackHandle&userNumber={userNumber}");
        }

        public async Task Fire(int userNumber)
        {
            await http .GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?handle_userNumber={userNumber}&level=1&alwaysRefire=false");
        }

        public async Task Level(int userNumber, float level)
        {
            await http.GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?handle_userNumber={userNumber}&level={level}&alwaysRefire=false");
        }

        public async Task Kill(int userNumber)
        {
            await http.GetAsync($"titan/script/2/Playbacks/KillPlayback?handle_userNumber={userNumber}");
        }

        public async Task KillAll()
        {
            await http.GetAsync($"titan/script/2/Playbacks/KillAllPlaybacks");
        }
    }
}

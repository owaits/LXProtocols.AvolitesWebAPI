using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class PlaybackFader
    {
        [Inject]
        public IAvolitesTitan Titan { get; set; }

        private int userNumber = 0;

        [Parameter]
        public int UserNumber
        {
            get { return userNumber; }
            set
            {
                if (userNumber != value)
                {
                    userNumber = value;
                    InvokeAsync(() => UpdatePlayback());
                }
            }
        }

        [Parameter]
        public string Legend { get; set; }

        private int level = 0;

        public int Level
        {
            get { return level; }
            set
            {
                if(level != value)
                {
                    level = value;
                    InvokeAsync(()=> UpdateLevel((float) level / 100f));
                }                
            }
        }

        public bool IsActive()
        {
            return PlaybackHandle?.Active == true;
        }

        public HandleInformation PlaybackHandle { get; set; }

        protected async Task UpdatePlayback()
        {
            if (UserNumber > 0)
            {
                PlaybackHandle = await Titan.API.Playbacks.GetHandleFromUserNumber(UserNumber);
                StateHasChanged();
            }
        }

        public string DisplayLegend()
        {
            if (string.IsNullOrEmpty(Legend))
            {
                return $"Playback {UserNumber}";
            }

            return Legend;
        }

        public async Task UpdateLevel(float level)
        {
            await Titan.API.Playbacks.Level(UserNumber,level);
        }
    }
}

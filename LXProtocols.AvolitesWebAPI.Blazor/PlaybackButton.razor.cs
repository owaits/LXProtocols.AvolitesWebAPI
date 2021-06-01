using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class PlaybackButton
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
                if(userNumber != value)
                {
                    userNumber = value;
                    InvokeAsync(() => UpdatePlayback());
                }
            }
        }

        [Parameter]
        public string Legend { get; set; }

        public bool Active { get; set; }

        public bool IsActive()
        {
            return PlaybackHandle?.Active == true;
        }

        public HandleInformation PlaybackHandle { get; set; }


        protected async Task UpdatePlayback()
        {
            if(UserNumber > 0)
            {
                PlaybackHandle = await Titan.API.Playbacks.GetHandleFromUserNumber(UserNumber);
                StateHasChanged();
            }
        }

        public string DisplayLegend()
        {
            if(string.IsNullOrEmpty(Legend))
            {
                if(PlaybackHandle != null)
                {
                    return PlaybackHandle.Legend;
                }
                return $"Playback {UserNumber}";
            }

            return Legend;
        }

        public string BackgroundImageCSS()
        {
            if (!string.IsNullOrEmpty(PlaybackHandle?.Icon))
            {
                return $"background-image: url('{PlaybackHandle.Icon}')";
            }

            return string.Empty;
        }

        public async Task Fire()
        {
            if(Active)
            {
                await Titan.API.Playbacks.Kill(UserNumber);
                Active = false;
            }
            else
            {
                await Titan .API.Playbacks.Fire(UserNumber);
                Active = true;                
            }

            await UpdatePlayback();
        }
    }
}

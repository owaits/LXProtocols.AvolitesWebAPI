using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class PlaybackButton
    {
        private Timer refreshTimer;

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

        private int titanId = 0;

        [Parameter]
        public int TitanId
        {
            get { return titanId; }
            set
            {
                if (titanId != value)
                {
                    titanId = value;
                    InvokeAsync(() => UpdatePlayback());
                }
            }
        }

        [Parameter]
        public string Legend { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        public bool Active { get; set; }

        public bool IsActive()
        {
            return PlaybackHandle?.Active == true;
        }

        public HandleInformation PlaybackHandle { get; set; }

        protected override async Task OnInitializedAsync()
        {
            refreshTimer = new Timer(new TimerCallback(RefreshTimer_Elapsed));
            refreshTimer.Change(TimeSpan.FromSeconds(15), Timeout.InfiniteTimeSpan);

            await Task.CompletedTask;
        }

        private void RefreshTimer_Elapsed(object sender)
        {
            InvokeAsync(async () =>
            {
                await UpdateActive();

                TimeSpan refreshPeriod = IsActive() ? TimeSpan.FromSeconds(5) : TimeSpan.FromSeconds(10);
                refreshTimer.Change(refreshPeriod, Timeout.InfiniteTimeSpan);
            });
        }

        protected async Task UpdatePlayback()
        {
            if (TitanId > 0)
            {
                PlaybackHandle = await Titan.API.Handles.GetHandleFromTitanId(TitanId);
                StateHasChanged();
            }
            else if (UserNumber > 0)
            {
                PlaybackHandle = await Titan.API.Handles.GetHandleFromUserNumber(UserNumber);
                StateHasChanged();
            }
        }

        protected async Task UpdateActive()
        {
            HandleInformation handle = null;
            if (TitanId > 0)
            {
                handle = await Titan.API.Handles.GetHandleFromTitanId(TitanId);
            }
            else if (UserNumber > 0)
            {
                handle = await Titan.API.Handles.GetHandleFromUserNumber(UserNumber);
            }

            if(handle?.Active != PlaybackHandle?.Active)
            {
                await UpdatePlayback();
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
                return $"background: url('{PlaybackHandle.Icon}') no-repeat center;";
            }

            return string.Empty;
        }

        public string BorderColourCSS()
        {
            if (!string.IsNullOrEmpty(PlaybackHandle?.Halo))
            {
                string cssColor = "#" + PlaybackHandle.Halo.Substring(3) + PlaybackHandle.Halo.Substring(1, 2);
                return $"border-color: {cssColor}";
            }

            return string.Empty;
        }

        public async Task Fire()
        {
            if(Active)
            {
                await Titan.API.Playbacks.Kill(HandleReference.FromAny(TitanId, UserNumber));
                Active = false;
            }
            else
            {
                await Titan.API.Playbacks.Fire(HandleReference.FromAny(TitanId, UserNumber));
                Active = true;
            }

            await UpdatePlayback();
        }
    }
}

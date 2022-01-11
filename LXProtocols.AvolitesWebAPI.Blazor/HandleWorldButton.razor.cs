using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class HandleWorldButton
    {
        [Inject]
        public IAvolitesTitan Titan { get; set; }

        private Guid worldId = Guid.Empty;

        [Parameter]
        public Guid WorldId 
        {
            get { return worldId; }
            set
            {
                if(worldId != value)
                {
                    worldId = value;
                    InvokeAsync(() => UpdateWorld());
                }
            }
        }

        [Parameter]
        public string Legend { get; set; }

        [Parameter]
        public Func<Task> AfterWorldChange { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        public bool Active { get; set; }

        public bool IsActive()
        {
            return false;
        }

        protected async Task UpdateWorld()
        {
            //if(UserNumber > 0)
            //{
            //    PlaybackHandle = await Titan.API.Handles.GetHandleFromUserNumber(UserNumber);
            //    StateHasChanged();
            //}
            await Task.CompletedTask;
        }

        public string DisplayLegend()
        {
            return Legend;
        }

        //public string BackgroundImageCSS()
        //{
        //    if (!string.IsNullOrEmpty(PlaybackHandle?.Icon))
        //    {
        //        return $"background-image: url('{PlaybackHandle.Icon}')";
        //    }

        //    return string.Empty;
        //}

        private async Task Fire()
        {
            await Titan.API.Handles.SetHandleWorld(WorldId);
            Active = true;

            await UpdateWorld();

            if (AfterWorldChange != null)
            {
                await AfterWorldChange();
            }
        }
    }
}

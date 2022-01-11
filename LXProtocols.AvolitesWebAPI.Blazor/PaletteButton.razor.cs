using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class PaletteButton
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
                    InvokeAsync(() => UpdatePalette());
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
                    InvokeAsync(() => UpdatePalette());
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
            return PaletteHandle?.Active == true;
        }

        public HandleInformation PaletteHandle { get; set; }


        protected async Task UpdatePalette()
        {
            if(TitanId > 0)
            {
                PaletteHandle = await Titan.API.Handles.GetHandleFromTitanId(TitanId, "paletteHandle");
                StateHasChanged();
            }
            else if(UserNumber > 0)
            {
                PaletteHandle = await Titan.API.Handles.GetHandleFromUserNumber(UserNumber, "paletteHandle");
                StateHasChanged();
            }
        }

        public string DisplayLegend()
        {
            if(string.IsNullOrEmpty(Legend))
            {
                if(PaletteHandle != null)
                {
                    return PaletteHandle.Legend;
                }
                return $"Playback {UserNumber}";
            }

            return Legend;
        }

        public string BackgroundImageCSS()
        {
            if (!string.IsNullOrEmpty(PaletteHandle?.Icon))
            {
                return $"background: url('{PaletteHandle.Icon}') no-repeat center;";
            }

            return string.Empty;
        }

        public string BorderColourCSS()
        {
            if (!string.IsNullOrEmpty(PaletteHandle?.Halo))
            {
                string cssColor = "#" + PaletteHandle.Halo.Substring(3) + PaletteHandle.Halo.Substring(1, 2);
                return $"border-color: {cssColor}";
            }

            return string.Empty;
        }


        public async Task Fire()
        {
            await Titan.API.Palettes.ApplyPalette(HandleReference.FromAny(TitanId, UserNumber));
            await UpdatePalette();
        }
    }
}

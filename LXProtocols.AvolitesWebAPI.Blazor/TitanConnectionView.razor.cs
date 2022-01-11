using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class TitanConnectionView
    {
        private Timer refreshTimer;

        [Inject]
        public IAvolitesTitan Titan { get; set; }

        [Parameter]
        public RenderFragment NotConnected { get; set; }

        [Parameter]
        public RenderFragment Connected { get; set; }

        [Parameter]
        public RenderFragment Connecting { get; set; }

        public bool? IsConnected { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if(!await UpdateConnection())
            {
                refreshTimer = new Timer(new TimerCallback(RefreshTimer_Elapsed));
                refreshTimer.Change(TimeSpan.FromSeconds(10),Timeout.InfiniteTimeSpan);
            }
        }

        private void RefreshTimer_Elapsed(object sender)
        {
            InvokeAsync(async () =>
            {
                if (await UpdateConnection())
                {
                    StateHasChanged();
                }
                else
                {
                    refreshTimer.Change(TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);
                }
            });
        }

        private async Task<bool> UpdateConnection()
        {
            IsConnected = await Titan.IsConnected();
            return IsConnected == true;
        }

        private void Refresh()
        {
            InvokeAsync(async () =>
            {
                await UpdateConnection();
                StateHasChanged();
            });
        }
    }
}

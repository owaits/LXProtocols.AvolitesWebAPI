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
    public partial class FixtureButton
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
                if (userNumber != value)
                {
                    userNumber = value;
                    InvokeAsync(() => UpdateFixture());
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
                    InvokeAsync(() => UpdateFixture());
                }
            }
        }

        [Parameter]
        public string Legend { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        public bool Selected { get; set; }

        public bool IsActive()
        {
            return FixtureHandle?.Active == true;
        }

        public bool IsSelected()
        {
            return FixtureHandle?.Selected == true;
        }

        public HandleInformation FixtureHandle { get; set; }

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

                TimeSpan refreshPeriod = IsSelected() ? TimeSpan.FromSeconds(5) : TimeSpan.FromSeconds(10);
                refreshTimer.Change(refreshPeriod, Timeout.InfiniteTimeSpan);
            });
        }

        protected async Task UpdateFixture()
        {
            if (TitanId > 0)
            {
                FixtureHandle = await Titan.API.Handles.GetHandleFromTitanId(TitanId);
                StateHasChanged();
            }
            else if (UserNumber > 0)
            {
                FixtureHandle = await Titan.API.Handles.GetHandleFromUserNumber(UserNumber);
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

            if (handle?.Active != FixtureHandle?.Active)
            {
                await UpdateFixture();
            }
        }

        public string DisplayLegend()
        {
            if (string.IsNullOrEmpty(Legend))
            {
                if (FixtureHandle != null)
                {
                    return FixtureHandle.Legend;
                }
                return $"Fixture {UserNumber}";
            }

            return Legend;
        }

        public string BackgroundImageCSS()
        {
            if (!string.IsNullOrEmpty(FixtureHandle?.Icon))
            {
                return $"background: url('{FixtureHandle.Icon}') no-repeat center;";
            }

            return string.Empty;
        }

        public string BorderColourCSS()
        {
            if (!string.IsNullOrEmpty(FixtureHandle?.Halo))
            {
                string cssColor = "#" + FixtureHandle.Halo.Substring(3) + FixtureHandle.Halo.Substring(1, 2);
                return $"border-color: {cssColor}";
            }

            return string.Empty;
        }

        public async Task Select()
        {
            if (!Selected)
            {
                await Titan.API.Selection.SelectFixturesFromHandles(new[] { HandleReference.FromAny(TitanId, UserNumber) });
                Selected = true;
            }

            await UpdateFixture();
        }
    }
}

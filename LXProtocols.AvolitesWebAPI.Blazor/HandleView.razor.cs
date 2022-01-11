using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class HandleView
    {
        [Inject]
        public IAvolitesTitan Titan { get; set; }

        private string group = "";

        [Parameter]
        public string Group 
        {
            get { return group; }
            set
            {
                if(group != value)
                {
                    group = value;
                    InvokeAsync(() => UpdateHandles());
                }
            }
        }

        [Parameter]
        public int Page { get; set; } = -1;

        [Parameter]
        public RenderFragment<HandleInformation> ChildContent { get; set; }

        public HandleInformation[] Handles { get; set; } = null;


        protected async Task UpdateHandles()
        {
            Handles = await Titan.API.Handles.GetHandles(group,Page);
            StateHasChanged();
        }

    }
}

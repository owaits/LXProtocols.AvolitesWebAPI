using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Blazor
{
    public partial class MacroEditor
    {
        [Inject]
        public IAvolitesTitan Titan { get; set; }

        public IEnumerable<string> Macros { get; set; }

        private string macroId = null;

        [Parameter]
        public string MacroId 
        {
            get { return macroId; }
            set
            {
                if(macroId != value)
                {
                    macroId = value;
                    InvokeAsync(() => LoadMacro(value));
                }
            }
        }

        [Parameter]
        public RenderFragment<string> Editor { get; set; }

        public string MacroScript { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Macros = await Titan.API.Macros.GetMacroIds(true);

            if(!string.IsNullOrEmpty(MacroId))
            {
                await LoadMacro(MacroId);
            }

            StateHasChanged();
        }

        protected async Task LoadMacro(string macroId)
        {
            MacroScript = await Titan.API.Macros.ExportMacro(macroId);
            StateHasChanged();
        }

        protected async Task SelectMacro(ChangeEventArgs e)
        {
            MacroId = e.Value.ToString();
        }

        protected async Task SaveMacro()
        {
            await Titan.API.Macros.ImportMacro(MacroScript);
        }

        protected async Task SaveAndRunMacro()
        {
            await SaveMacro();
            await Titan.API.Macros.RecallMacro(MacroId);
        }

        protected async Task NewMacro()
        {
            await SaveMacro();
            await Titan.API.Macros.RecallMacro(MacroId);
        }
    }
}

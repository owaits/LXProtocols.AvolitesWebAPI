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
        private bool newMacroMode = false;

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

                    if(!newMacroMode)
                    {
                        InvokeAsync(() => LoadMacro(value));
                    }

                    MacroIdChanged.InvokeAsync(macroId);
                }
            }
        }

        [Parameter]
        public EventCallback<string> MacroIdChanged { get; set; }

        [Parameter]
        public RenderFragment<string> Editor { get; set; }

        [Parameter]
        public Action<string> OnMacroScriptChanged { get; set; }

        public string MacroName { get; set; }

        public string MacroDescription { get; set; }

        private string macroScript = null;

        public string MacroScript 
        {
            get { return macroScript; }
            set
            {
                if(macroScript != value)
                {
                    macroScript = value;

                }
            }
        }

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

            if (OnMacroScriptChanged != null)
                OnMacroScriptChanged(MacroScript);

            StateHasChanged();
        }

        protected async Task SelectMacro(ChangeEventArgs e)
        {
            MacroId = e.Value.ToString();
        }

        protected async Task SaveMacro()
        {
            if(!string.IsNullOrEmpty(MacroScript))
            {
                var script = MacroScript;

                if (!script.Contains("<avolites.macros>"))
                    script = "<avolites.macros>\r\n" + MacroScript + "</avolites.macros>\r\n";

                await Titan.API.Macros.ImportMacro(script);
            }            
        }

        protected async Task SaveAndRunMacro()
        {
            await SaveMacro();
            await Titan.API.Macros.RecallMacro(MacroId);
        }

        protected async Task NewMacro()
        {
            MacroId = string.Empty;
            newMacroMode = true;
            StateHasChanged();
        }

        protected async Task FinishNewMacro()
        {
            if(!string.IsNullOrEmpty(MacroId))
            {
                MacroScript = $"<avolites.macros>\r\n<macro name=\"{MacroName}\" id=\"{MacroId}\">\r\n    <description>{MacroDescription}</description>\r\n    <sequence>\r\n      <step></step>\r\n    </sequence>\r\n  </macro>\r\n</avolites.macros>";
                
                if (OnMacroScriptChanged != null)
                    OnMacroScriptChanged(MacroScript);
            }

            newMacroMode = false;
            StateHasChanged();
        }
    }
}

using LXProtocols.AvolitesWebAPI.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    public class Macros
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Playbacks"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Macros(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Gets the macro ids for macros in the current show.
        /// </summary>
        /// <param name="includeUnassignedHandles">if set to <c>true</c> [include unassigned handles].</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetMacroIds (bool includeUnassignedHandles = false)
        {            
            return await http.GetFromJsonAsync<IEnumerable<string>>($"titan/script/2/UserMacros/GetAllMacroIds?includeUnassignedHandles={includeUnassignedHandles}");
        }

        /// <summary>
        /// Exports the macro the specified macro from the show and return the macro as XML.
        /// </summary>
        /// <param name="macroId">The ID of the macro to export.</param>
        /// <returns>THe macro in XML format.</returns>
        public async Task<string> ExportMacro(string macroId)
        {
            return await http.GetFromJsonAsync<string>($"titan/script/2/UserMacros/ExportXml?macroId={macroId}");
        }

        private class ImportArguments
        {
            public string script { get; set; }
        }

        /// <summary>
        /// Imports the XML macro script into the current show so it can be used in that show.
        /// </summary>
        /// <param name="macroScript">The XML macro script containing the macro to be imported.</param>
        public async Task ImportMacro(string macroScript)
        {
            await http.PostAsJsonAsync($"titan/script/2/UserMacros/ImportXml", new ImportArguments() { script = macroScript });
        }

        /// <summary>
        /// Recalls the macro adn runs it within the current show.
        /// </summary>
        /// <param name="macroId">The ID of the macro to recall and run.</param>
        public async Task RecallMacro(string macroId)
        {
            await http.GetAsync($"titan/script/2/UserMacros/RecallMacroById?macroId={macroId}");
        }
    }
}

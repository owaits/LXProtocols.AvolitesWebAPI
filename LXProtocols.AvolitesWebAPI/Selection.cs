using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{

    /// <summary>
    /// Implements WebAPI methods related to election of fixtures.
    /// </summary>
    public class Selection
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Playbacks"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Selection(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Allows you to select a range fixture in the editor, when you select a fixture you can modify values for the selected fixtures.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public async Task SelectFixturesFromHandles(IEnumerable<HandleReference> handle)
        {
            await http.GetAsync($"titan/script/2/Selection/Context/Programmer/SelectFixturesFromHandles?{handle.ToQueryArgument("handleList")}");
        }

        /// <summary>
        /// Clears the current selection and creates a restore point.
        /// </summary>
        public async Task Clear()
        {
            await http.GetAsync($"titan/script/2/Selection/Context/Programmer/Clear");
        }

    }
}

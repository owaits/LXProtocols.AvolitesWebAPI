using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// The menu system in Titan supports much of the operation of the console through to handling key presses and displaying menus.
    /// </summary>
    /// <remarks>
    /// Many of these commands will operate on the WebAPI users, the default is that WebAPI operates on its own user and so many of these methods may appear to have no effect
    /// as they are not operating on the consoles user.
    /// </remarks>
    public class Menu
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fixtures"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Menu(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Inject an input to the menu system. Inputs are button hardware actions such as button presses and fader movements and pseudo actions such as fixture handle press or palette key press
        /// </summary>
        /// <param name="type">Type of the input. (OnButtonDown, OnButtonUp, etc)</param>
        /// <param name="id">The id of the input.</param>
        /// <param name="group">The panel group or region of the input.</param>
        /// <param name="index">The index of the input in that group.</param>
        public async Task InjectInput(string type, string id, string group, int index)
        {
            await http.GetAsync($"titan/script/2/Menu/InjectInput?type={type}&id={id}&group={group}&index={index}");
        }
    }
}

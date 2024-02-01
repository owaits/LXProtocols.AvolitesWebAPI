using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Implements WebAPI methods related to placing values in the programmer and updating the programmer.
    /// </summary>
    public class Programmer
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Playbacks"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Programmer(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Sets all attributes in the selected fixtures to their locate values. For a moving head this is normally straight down and open white.
        /// </summary>
        public async Task LocateSelectedFixtures()
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/LocateSelectedFixtures?allAttributes=true");
        }

        /// <summary>
        /// Sets the dimmer level of the selected fixtures to the given level.
        /// </summary>
        /// <param name="level">The level as a percentage.</param>
        public async Task SetDimmerLevel(double level)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetDimmerLevel?level={level}");
        }

        /// <summary>
        /// Sets the attribute current value of all currently selected fixtures.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="value">The value to set the control to where 1 is full and 0 is off..</param>
        public async Task SetControlValue(string controlName, string functionName, double value)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetControlValueByName?controlName={controlName}&functionName={functionName}&value={value}&programmer=true&createRestorePoint=false");
        }

        /// <summary>
        /// Sets the selected fixture colour mix channels to levels to recreate the specified HSI value.
        /// </summary>
        /// <param name="hue">The hue.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="intensity">The intensity.</param>
        public async Task SetColourControlHSI(double hue, double saturation, double intensity)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetColourControlValues?hsi={hue},{saturation},{intensity}&programmer=true&createRestorePoint=false");
        }

        /// <summary>
        /// Sets the selected dimmer x fade.
        /// </summary>
        /// <param name="xFadeOn">if set to true [x fade on].</param>
        public async Task SetSelectedDimmerxFade(bool xFadeOn)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetSelectedDimmerxFade?xFadeOn={xFadeOn}");
        }

    }
}

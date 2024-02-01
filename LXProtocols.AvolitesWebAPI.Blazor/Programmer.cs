using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
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


        public async Task Locate()
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/LocateSelectedFixtures?allAttributes=true");
        }

        public async Task SetDimmerLevel(double level)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetDimmerLevel?level={level}");
        }

        public async Task SetControlValue(string controlName, string functionName, double value)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetControlValueByName?controlName={controlName}&functionName={functionName}&value={value}&programmer=true&createRestorePoint=false");
        }

        public async Task SetColourControlHSI(double hue, double saturation, double intensity)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetColourControlValues?hsi={hue},{saturation},{intensity}&programmer=true&createRestorePoint=false");
        }

        public async Task SetSelectedDimmerxFade(bool xFadeOn)
        {
            await http.GetAsync($"titan/script/2/Programmer/Editor/Fixtures/SetSelectedDimmerxFade?xFadeOn={xFadeOn}");
        }

    }
}

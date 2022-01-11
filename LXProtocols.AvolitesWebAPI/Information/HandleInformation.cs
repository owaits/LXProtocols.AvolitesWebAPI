using System;
using System.Collections.Generic;
using System.Text;

namespace LXProtocols.AvolitesWebAPI
{
    public class HandleInformation
    {
        public int TitanId { get; set; }

        public string Type { get; set; }

        public string Legend { get; set; }

        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the halo colour for this handle in the form #AARRGGBB
        /// </summary>
        /// <remarks>
        /// The halo colour is represented as a hex number in the form #AARRGGBB with Alpha, Red, Green and Blue components.
        /// 
        /// Please take note that CSS colours are in the form #RRGGBBAA and so the halo string
        /// will require converting when used with CSS.
        /// </remarks>
        public string Halo { get; set; }

        public string Icon { get; set; }

        public bool Selected { get; set; }

        public bool Active { get; set; }
    }
}

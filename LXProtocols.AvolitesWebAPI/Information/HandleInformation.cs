using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace LXProtocols.AvolitesWebAPI
{
    public class HandleInformation
    {
        /// <summary>
        /// Gets or sets the titan ID for the item linked to this handle..
        /// </summary>
        public int TitanId { get; set; }

        /// <summary>
        /// Gets or sets the type of this handle such a fixtureHandle, cueListHandle etc.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the legend of the item linked to this handle.
        /// </summary>
        public string Legend { get; set; }

        /// <summary>
        /// Gets or sets the notes related to this handle.
        /// </summary>
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

        /// <summary>
        /// Gets the icon for this handle.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets whether this handle has been selected.
        /// </summary>
        /// <remarks>
        /// This will indicate whether the handle is selected by WebAPI not nesaserily by the console.
        /// </remarks>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets whether this handle is considered active such as with a playback whether it is loaded.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets additional information specific to this handle type.
        /// </summary>
        public JsonObject[] Information { get; set; }
    }
}

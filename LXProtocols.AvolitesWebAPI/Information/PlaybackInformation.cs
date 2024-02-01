using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Information
{
    /// <summary>
    /// Contains information relating to a playback.
    /// </summary>
    public class PlaybackInformation
    {
        /// <summary>
        /// Gets or sets the ID of the playback.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the playback icon.
        /// </summary>
        public int IconId { get; set; }

        /// <summary>
        /// Gets or sets the legend for this playback.
        /// </summary>
        public string Legend { get; set; }

        /// <summary>
        /// Gets whether the playback has been loaded.
        /// </summary>
        public bool Loaded { get; set; }

        /// <summary>
        /// Gets the priority of this playback.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets a list of cue IDs for cues in this playback
        /// </summary>
        public int[] Cues { get; set; }
    }
}

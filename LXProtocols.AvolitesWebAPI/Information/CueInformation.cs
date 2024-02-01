using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Information
{
    /// <summary>
    /// Contains information relating to a cue within a cue list.
    /// </summary>
    public class CueInformation
    {
        /// <summary>
        /// Gets or sets the Titan Id for this cue.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the legend of the cue.
        /// </summary>
        public string Legend { get; set; }

        /// <summary>
        /// Gets or sets the cue number for the cue.
        /// </summary>
        public float CueNumber { get; set; }
    }
}

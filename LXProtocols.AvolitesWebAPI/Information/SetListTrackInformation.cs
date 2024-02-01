using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Information
{
    /// <summary>
    /// Contains information relating to a SetList track.
    /// </summary>
    public class SetListTrackInformation
    {
        /// <summary>
        /// Gets or sets the ID of the SetList.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the SetList icon.
        /// </summary>
        public int IconId { get; set; }

        /// <summary>
        /// Gets or sets the legend for this SetList.
        /// </summary>
        public string Legend { get; set; }
    }
}

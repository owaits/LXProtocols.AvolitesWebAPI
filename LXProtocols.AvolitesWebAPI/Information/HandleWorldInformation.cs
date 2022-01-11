using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Information
{
    /// <summary>
    /// Information about a handle world containing a subset of handles within the Titan system.
    /// </summary>
    public class HandleWorldInformation
    {
        /// <summary>
        /// The unique ID for the world and used when refering to this world.
        /// </summary>
        public Guid WorldId { get; set; }

        /// <summary>
        /// Gets or sets the user displayable legend for the handle world.
        /// </summary>
        public string Legend { get; set; }
    }
}

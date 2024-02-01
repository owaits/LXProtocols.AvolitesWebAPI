using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.JsonConverters
{
    /// <summary>
    /// Allows information to be read where the information is formatted in the JSON as a property of the main JSON object.
    /// </summary>
    /// <typeparam name="TInformation">The type of the information.</typeparam>
    internal class JsonInformation<TInformation>
    {
        /// <summary>
        /// Gets or sets the JSON information contained within the returned JSON..
        /// </summary>
        public TInformation Information { get; set; }
    }
}

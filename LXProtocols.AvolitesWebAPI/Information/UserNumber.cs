using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Information
{
    /// <summary>
    /// The user number is a user understandable number that an operator may use to refer to different elements within a Titan show.
    /// </summary>
    internal class UserNumber:HandleReference
    {
        public UserNumber(double number)
        {
            Number = number;
        }

        /// <summary>
        /// Gets or sets the user number.
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// Creates a query parameter that can be used to call a WebAPI method that references this handle.
        /// </summary>
        /// <remarks>
        /// Create a query parameter that can be used in a web API call in the format "handle_userNumber=1" or "handle_titanId=1".
        /// </remarks>
        /// <param name="argumentName">The name of the query parameter often "handle".</param>
        /// <returns>The encoded query parameter.</returns>
        public string ToQueryArgument(string argumentName)
        {
            return $"{argumentName}_userNumber={Number}";
        }
    }
}

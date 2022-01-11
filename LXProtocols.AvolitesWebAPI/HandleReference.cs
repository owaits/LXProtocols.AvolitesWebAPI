using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LXProtocols.AvolitesWebAPI.Information;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Allows you to reference an element within a Titan show using various methods such as Titan ID or user number.
    /// </summary>
    public interface HandleReference
    {
        /// <summary>
        /// Creates a query parameter that can be used to call a WebAPI method that references this handle.
        /// </summary>
        /// <remarks>
        /// Create a query parameter that can be used in a web API call in the format "handle_userNumber=1" or "handle_titanId=1".
        /// </remarks>
        /// <param name="argumentName">The name of the query parameter often "handle".</param>
        /// <returns>The encoded query parameter.</returns>
        string ToQueryArgument(string argumentName);

        /// <summary>
        /// Creates a handle reference from a titan ID.
        /// </summary>
        /// <param name="titanId">The titan ID to create the handle reference from.</param>
        /// <returns>The handle reference to return.</returns>
        public static HandleReference FromTitanId(int titanId)
        {
            return new TitanId(titanId);
        }

        /// <summary>
        /// Creates a handle reference from a user number.
        /// </summary>
        /// <param name="userNumber">The user number to create the handle reference from.</param>
        /// <returns>The handle reference to return.</returns>
        public static HandleReference FromUserNumber(double userNumber)
        {
            return new UserNumber(userNumber);
        }

        /// <summary>
        /// Creates a handle reference from a user number or titan ID with preference given to a Titan ID.
        /// </summary>
        /// <param name="userNumber">The user number to create the handle reference from.</param>
        /// <param name="titanId">The titan ID to create the handle reference from.</param>
        /// <returns>The handle reference to return.</returns>
        public static HandleReference FromAny(int titanId, double userNumber)
        {
            if (titanId > 0)
                return FromTitanId(titanId);
            else
                return FromUserNumber(userNumber);
        }
    }
}

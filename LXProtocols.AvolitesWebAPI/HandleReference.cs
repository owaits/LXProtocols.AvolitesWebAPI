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
        /// Creates a list handle reference from a list of titan ID.
        /// </summary>
        /// <param name="titanId">The list of titan IDs to create the handle references from.</param>
        /// <returns>A list of handle references to return.</returns>
        public static IEnumerable<HandleReference> FromTitanIds(IEnumerable<int> titanId)
        {
            return titanId.Select(id => new TitanId(id));
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
        /// Creates a list handle reference from a list of user numbers.
        /// </summary>
        /// <param name="titanId">The list of user numbers to create the handle references from.</param>
        /// <returns>A list of handle references to return.</returns>
        public static IEnumerable<HandleReference> FromUserNumbers(IEnumerable<double> userNumber)
        {
            return userNumber.Select(u=> new UserNumber(u));
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

    /// <summary>
    /// Extension methods useful for dealing with handle references.
    /// </summary>
    public static class HandleReferenceExtensions
    {
        /// <summary>
        /// Formats a list of handles into a suitable format for use within a url request.
        /// </summary>
        /// <param name="handles">The handles to add to the query.</param>
        /// <param name="argumentName">Name of the argument to place in the query..</param>
        /// <returns>The query argument.</returns>
        public static string ToQueryArgument(this IEnumerable<HandleReference> handles, string argumentName)
        {
            var userNumbers = handles.OfType<UserNumber>();
            if(userNumbers.Any())
                return UserNumber.ToQueryArgument(userNumbers, argumentName);

            var titanIds = handles.OfType<TitanId>();
            if (titanIds.Any())
                return TitanId.ToQueryArgument(titanIds, argumentName);

            return string.Empty;
        }
    }
}

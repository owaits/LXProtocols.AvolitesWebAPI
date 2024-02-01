using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI.Information
{
    /// <summary>
    /// The titan ID is a unique reference for all elements of a Titan show file, you should use it when refering to any show element.
    /// </summary>
    public class TitanId:HandleReference
    {
        public TitanId(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the Titan ID.
        /// </summary>
        public int Id { get; set; }

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
            return $"{argumentName}_titanId={Id}";
        }

        public static string ToQueryArgument(IEnumerable<TitanId> handles, string argumentName)
        {
            return $"{argumentName}_titanIdList={string.Join(",", handles.Select(h => h.Id))}";
        }
    }
}

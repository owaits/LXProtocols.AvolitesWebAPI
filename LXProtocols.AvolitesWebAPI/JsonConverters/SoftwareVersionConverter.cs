using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace LXProtocols.AvolitesWebAPI.JsonConverters
{
    /// <summary>
    /// Provides a conversion from the json software version forat and a version class.
    /// </summary>
    /// <remarks>
    /// THis conversion is required as the Titan version format does not match.
    /// </remarks>
    /// <seealso cref="System.Text.Json.Serialization.JsonConverter&lt;System.Version&gt;" />
    internal class SoftwareVersionConverter: JsonConverter<Version>
    {
        /// <summary>
        /// Reads and converts the JSON to type <typeparamref name="T" />.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public override Version Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int major = 0, minor = 0, build = 0, revision = 0;

            while(reader.Read())
            {
                if (reader.TokenType != JsonTokenType.PropertyName && reader.TokenType != JsonTokenType.StartObject)
                    break;

                string propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "_Major":
                    case "Major":
                        major = reader.GetInt32();
                        break;
                    case "_Minor":
                    case "Minor":
                        minor = reader.GetInt32();
                        break;
                    case "_Build":
                    case "Build":
                        build = reader.GetInt32();
                        break;
                    case "_Revision":
                    case "Revision":
                        revision = reader.GetInt32();
                        break;
                }
            }

            return new Version(major, minor, build, revision); 
        }

        /// <summary>
        /// Writes a specified value as JSON.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, Version value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}

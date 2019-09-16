using System;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Indigestion
{
    /// <summary>
    /// This class serializes precipitation data into JSON
    /// </summary>
    public class Outgester
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Outgester()
        {
            // NOP
        }

        /// <summary>
        /// Serializes precipitation data into JSON
        /// </summary>
        /// <param name="precipitationData">Precipitation data to be serialized</param>
        /// <returns>JSON if the precipitation data is not null; otherwise null</returns>
		public virtual string serialize(HistoricalPrecipitation precipitationData)
		{
            if (precipitationData == null)
            {
                return null;
            }

			DataContractJsonSerializer serializer =
				new DataContractJsonSerializer(typeof(HistoricalPrecipitation));
			MemoryStream stream = new MemoryStream();
			serializer.WriteObject(stream, precipitationData);
			stream.Position = 0;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				return streamReader.ReadLine();
			}
		}
    }
}

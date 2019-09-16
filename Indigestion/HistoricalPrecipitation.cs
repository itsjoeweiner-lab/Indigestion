using System;
using System.Runtime.Serialization;

namespace Indigestion
{
    /// <summary>
    /// Holds precipitation data for a single date
    /// </summary>
    [DataContract]
    public class HistoricalPrecipitation
    {

        [DataMember]
		public string Station { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public double Latitude { get; set; }
		[DataMember]
		public double Longitude { get; set; }
		[DataMember]
		public double Elevation { get; set; }
		[DataMember]
		public DateTime RecordedDate { get; set; }
		[DataMember]
		public double Precipitation { get; set; }

        /// <summary>
        /// Default constructor - no values are populated for member variables
        /// </summary>
        public HistoricalPrecipitation()
        {
            // NOP
        }

        /// <summary>
        /// Constructor which accepts values for all member variables
        /// </summary>
        /// <param name="station">The station value</param>
        /// <param name="name">The name value</param>
        /// <param name="latitude">The latitude value</param>
        /// <param name="longitude">The longitude value</param>
        /// <param name="elevation">The elevation value</param>
        /// <param name="date">The date value</param>
        /// <param name="precipitation">The precipitation value</param>
        public HistoricalPrecipitation(string station,
										 string name,
										 double latitude,
										 double longitude,
										 double elevation,
										 DateTime date,
										 double precipitation)
        {
			Station = station;
			Name = name;
			Latitude = latitude;
			Longitude = longitude;
			Elevation = elevation;
			RecordedDate = date;
			Precipitation = precipitation;
        }
    }
}

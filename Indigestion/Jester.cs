using System;
using System.Collections.Generic;

namespace Indigestion
{
    /// <summary>
    /// This class serves two purposes:
    ///    1. Holds the ingested data
    ///    2. Allows invokers to search for a precipitation prediction using a dateß
    /// </summary>
    public class Jester
    {
		public List<HistoricalPrecipitation> PrecipitationHistory = new List<HistoricalPrecipitation>();

        /// <summary>
        /// Default constructor
        /// </summary>
		public Jester()
		{
            // NOP
		}

        /// <summary>
        /// Precipitation data is added to the collection.
        /// </summary>
        /// <param name="data">Precipitation data to add</param>
        public void addPrecipitationData(HistoricalPrecipitation data)
		{
			PrecipitationHistory.Add(data);
		}

        /// <summary>
        /// Precipitation data is added to the collection.
        /// </summary>
        /// <param name="station">The station value as a string</param>
        /// <param name="name">The name value as a string</param>
        /// <param name="latitude">The latitude value as a double</param>
        /// <param name="longitude">The longitude value as a double</param>
        /// <param name="elevation">The elevation value as a double</param>
        /// <param name="date">The recorded date as a DateTime</param>
        /// <param name="precipitation">The precipitation value as a double</param>
        public void addPrecipitationData(string station,
                                         string name,
                                         double latitude,
                                         double longitude,
                                         double elevation,
                                         DateTime date,
                                         double precipitation)
		{
			addPrecipitationData(
                new HistoricalPrecipitation(station, name, latitude,
                                            longitude, elevation, date, precipitation));
		}

        /// <summary>
        /// Returns a precipitation prediction based upon a date
        /// </summary>
        /// <param name="date">The date to use when searching for a precipitation prediction</param>
        /// <returns>the precipitation prediction</returns>
        public virtual HistoricalPrecipitation predict(DateTime date)
		{
            return PrecipitationHistory.Find((prec) => prec.RecordedDate.Equals(date)); 
		}
    }
}

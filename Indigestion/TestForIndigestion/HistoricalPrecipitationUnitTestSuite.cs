using System;
using Indigestion;
using NUnit.Framework;

namespace Tests
{

    /// <summary>
    /// Ensures the HistoricalPrecipitation class properly stores the data for
    /// a single precipitation record.
    /// </summary>
    public class HistoricalPrecipitationUnitTestSuite
    {

        /// <summary>
        /// Test ensures that the getters and setters for each piece of data work as expected.
        /// </summary>
        [Test]
        public void testGettersAndSetters()
        {
            HistoricalPrecipitation precipitation = new HistoricalPrecipitation(STATION,
                                                                                NAME,
                                                                                LATITUDE,
                                                                                LONGITUDE,
                                                                                ELEVATION,
                                                                                RECORDED_DATE,
                                                                                PRECIPITATION);
            Assert.AreEqual(STATION, precipitation.Station);
            Assert.AreEqual(NAME, precipitation.Name);
            Assert.AreEqual(LONGITUDE, precipitation.Longitude);
            Assert.AreEqual(LATITUDE, precipitation.Latitude);
            Assert.AreEqual(ELEVATION, precipitation.Elevation);
            Assert.AreEqual(RECORDED_DATE, precipitation.RecordedDate);
            Assert.AreEqual(PRECIPITATION, precipitation.Precipitation);
        }

        private const double LATITUDE = 1.0, LONGITUDE = 2.0, ELEVATION = 3.0, PRECIPITATION = 4.0;
        private const string STATION = "station", NAME = "name";
        private readonly DateTime RECORDED_DATE = DateTime.Now;
    }
}

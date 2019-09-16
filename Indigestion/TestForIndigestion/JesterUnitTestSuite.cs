using System;
using Indigestion;
using NUnit.Framework;

namespace Tests
{

    /// <summary>
    /// Tests ensure that the Jester class works to hold the data as well as search for precipitation data.
    /// </summary>
    public class JesterUnitTestSuite
    {

        /// <summary>
        /// Ensures the search returns null precipitation data when the data collection contains no data.
        /// </summary>
        [Test]
        public void testPredictWithNoPrecipitationData()
        {
            Jester predictor = new Jester();
            Assert.IsEmpty(predictor.PrecipitationHistory);
            HistoricalPrecipitation prediction = predictor.predict(DateTime.Now);
            Assert.IsNull(prediction);
        }

         /// <summary>
         /// Ensures the search returns precipitation data when the data collection
         /// contains data.The two methods for adding data to the collection
         /// are tested.
         /// </summary>
        [Test]
        public void testPredictWithPrecipitationData()
        {
            DateTime expectedDate = new DateTime(2019, 2, 14);
            DateTime actualDate = new DateTime(2019, 2, 14);
            double actualPrecipitation = 8.0;
            double expectedPrecipitation = 8.0;

            Jester predictor = new Jester();
            HistoricalPrecipitation prediction1 = new HistoricalPrecipitation("station", "identification", 1.0, 2.0, 3.0, DateTime.Now, 4.0);
            predictor.addPrecipitationData(prediction1);
            predictor.addPrecipitationData("radio", "station", 5.0, 6.0, 7.0, actualDate, actualPrecipitation);

            Assert.IsTrue(predictor.PrecipitationHistory.Count == 2);

            HistoricalPrecipitation precipitation = predictor.predict(actualDate);

            Assert.IsNotNull(precipitation);
            Assert.AreEqual(expectedDate, precipitation.RecordedDate);
            Assert.AreEqual(expectedPrecipitation, precipitation.Precipitation);
        }
    }
}

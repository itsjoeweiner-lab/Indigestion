using System;
using Indigestion;
using NUnit.Framework;

namespace Tests
{

    /// <summary>
    /// Tests ensure the serialization process works to producd a JSON document
    /// when presented with precipitation data.
    /// </summary>
    public class OutgesterUnitTestSuite
    {

        /// <summary>
        /// Ensures a null string is returned when a null is passed as input.
        /// </summary>
        [Test]
        public void testSerializeWithNullPrecipitationData()
        {
            Outgester serializer = new Outgester();
            string json = serializer.serialize(null);
            Assert.IsNull(json);
        }

        /// <summary>
        /// Ensures a string containing JSON is returned when precipitation data
        /// is passed as input.
        /// </summary>
        [Test]
        public void testSerializeWithPrecipitationData()
        {
            Outgester serializer = new Outgester();
            HistoricalPrecipitation prediction
                = new HistoricalPrecipitation("station", "identification", 1.0, 2.0, 3.0, DateTime.Now, 4.0);
            string json = serializer.serialize(prediction);
            Assert.IsNotNull(json);
        }
    }
}

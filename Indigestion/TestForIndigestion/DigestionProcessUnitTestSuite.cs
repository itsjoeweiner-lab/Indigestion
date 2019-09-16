using System;
using Indigestion;
using NUnit.Framework;
using Moq;

namespace Tests
{

    /// <summary>
    /// Ensures that the main processor orchestrates the ingestion, prediciton and
    /// serialization of the data to JSON. The DigestionProcess class has a few
    /// dependencies. Mock objects (via Moq) are created for the dependencies.
    /// </summary>
    public class DigestionProcessUnitTestSuite
    {

         /// <summary>
         /// Test ensures all three steps in the process are invoked:
         ///   1. ingestion of input data
         ///   2. precipitation prediction
         ///   3. serialization to json
         /// Step #1 should occur as the file path won't equal the "last file path".
         /// </summary>
        [Test]
        public void testProcessNewFilePath()
        {
            string filePath = "abc.csv";
            DateTime date = DateTime.Now;
            string expectedJson = "json{}";

            Mock<Jester> mockJester = new Mock<Jester>();
            Mock<Ingester> mockIngester = new Mock<Ingester>();
            Mock<Outgester> mockOutgester = new Mock<Outgester>();
            Mock<HistoricalPrecipitation> precipitation = new Mock<HistoricalPrecipitation>();

            mockJester.Setup(x => x.predict(date)).Returns(() => precipitation.Object);
            mockOutgester.Setup(outg => outg.serialize(precipitation.Object)).Returns(expectedJson);
            mockIngester.Setup(ing => ing.ingest(filePath)).Verifiable();

            DigestionProcess processor = new DigestionProcess(mockJester.Object, mockIngester.Object, mockOutgester.Object);
            string actualJson = processor.process(date, filePath);

            Assert.AreEqual(expectedJson, actualJson);
            mockJester.Verify(jes => jes.predict(date));
            mockIngester.Verify(ing => ing.ingest(filePath));
            mockOutgester.Verify(outg => outg.serialize(precipitation.Object));
        }

         /// <summary>
         /// Test ensures two of the three steps in the process are invoked:
         ///   1. precipitation prediction
         ///   2. serialization to json
         /// Ingestion should* not* occur as the file path equals the "last file path". 
         /// </summary>
        [Test]
        public void testProcessWithExistingFilePath()
        {
            string filePath = "abc.csv"; 
            DateTime date = DateTime.Now;
            string expectedJson = "json{}";

            Mock<Jester> mockJester = new Mock<Jester>();
            Mock<Ingester> mockIngester = new Mock<Ingester>();
            Mock<Outgester> mockOutgester = new Mock<Outgester>();
            Mock<HistoricalPrecipitation> precipitation = new Mock<HistoricalPrecipitation>();

            mockJester.Setup(x => x.predict(date)).Returns(() => precipitation.Object);
            mockOutgester.Setup(outg => outg.serialize(precipitation.Object)).Returns(expectedJson);
            mockIngester.Setup(ing => ing.ingest(It.IsAny<string>())).Throws(new AssertionException(""));

            DigestionProcess processor = new DigestionProcess(mockJester.Object, mockIngester.Object, mockOutgester.Object);
            processor.LastFilePath = filePath;
            string actualJson = processor.process(date, filePath);

            Assert.AreEqual(expectedJson, actualJson);
            mockJester.Verify(jes => jes.predict(date));
            mockOutgester.Verify(outg => outg.serialize(precipitation.Object));
            mockIngester.Verify(ing => ing.ingest(filePath), Times.Never);
        }
    }
}

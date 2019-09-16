using System;
using Indigestion;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{

     /// <summary>
     /// Tests ensure the data ingestion process works as expected.  Tests
     /// assume that source data arrives as a string[] (reading from
     /// a file is mocked).
     ///
     /// ** Note: this class is light on tests - isValid() could be better tested
     /// </summary>
    public class IngesterUnitTestSuite
    {

        /// <summary>
        /// Ensures that no data is ingested when no source data is available.
        /// </summary>
        [Test]
        public void testImportNoData()
        {
            Jester dataCollector = new Jester();
            Mock<FileContentImporter> mockImporter = new Mock<FileContentImporter>();

            Ingester dataIngester = new Ingester();
            dataIngester.Collector = dataCollector;
            dataIngester.Importer = mockImporter.Object;

            string[] data = new string[0];
            string filePath = "abc.csv";
            mockImporter.Setup(imp => imp.import(filePath)).Returns(data).Verifiable();

            dataIngester.ingest(filePath);

            Assert.IsEmpty(dataCollector.PrecipitationHistory);
            mockImporter.Verify(imp => imp.import(filePath), Times.Once);
        }

        /// <summary>
        /// Ensures that no data is ingested when incorrectly formatted or otherwise invalid data is encountered.
        /// </summary>
        [Test]
        public void testImportAllInvalidData()
        {
            Jester dataCollector = new Jester();
            Mock<FileContentImporter> mockImporter = new Mock<FileContentImporter>();

            Ingester dataIngester = new Ingester();
            dataIngester.Collector = dataCollector;
            dataIngester.Importer = mockImporter.Object;

            string[] data = new string[1];
            data[0] = "a,b,c";
            string filePath = "abc.csv";
            mockImporter.Setup(imp => imp.import(filePath)).Returns(data).Verifiable();

            dataIngester.ingest(filePath);

            Assert.IsEmpty(dataCollector.PrecipitationHistory);
            mockImporter.Verify(imp => imp.import(filePath), Times.Once);
        }

        /// <summary>
        /// Ensures that properly formatted, valid data is ingested.
        /// </summary>
        [Test]
        public void testImportValidData()
        {
            Jester dataCollector = new Jester();
            Mock<FileContentImporter> mockImporter = new Mock<FileContentImporter>();

            Ingester dataIngester = new Ingester();
            dataIngester.Collector = dataCollector;
            dataIngester.Importer = mockImporter.Object;

            string[] data = new string[1];
            DateTime expectedDate = new DateTime(2019, 10, 1);
            data[0] = "a,\"b,c\",1.0,2.0,3.0," + expectedDate + ",4.0";
            string filePath = "abc.csv";
            mockImporter.Setup(imp => imp.import(filePath)).Returns(data).Verifiable();

            dataIngester.ingest(filePath);

            List<HistoricalPrecipitation> collector = dataCollector.PrecipitationHistory;
            Assert.IsNotEmpty(collector);
            Assert.IsTrue(collector.Count == 1);
            Assert.AreEqual(expectedDate, collector[0].RecordedDate);

            mockImporter.Verify(imp => imp.import(filePath), Times.Once);
        }
    }
}

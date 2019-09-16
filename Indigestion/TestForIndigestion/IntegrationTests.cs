using NUnit.Framework;
using Indigestion;
using System;

namespace Tests
{

    /// <summary>
    /// This integration test ensures that the entire set of code
    /// works for the main success use case.  It acts like a sniff test.
    /// </summary>
    public class IntegrationTests
    {

        /// <summary>
        /// Ensures the main success use case works with data ingested from a file and the current date.
        /// </summary>
        [Test]
        public void IntegrationTestSuccess()
        {
            string filePath = "../../../../data.csv";
            DigestionProcess process = new DigestionProcess();
            string json = process.process(DateTime.Now, filePath);
            Assert.IsNotEmpty(json);
        }
    }
}
using System;

namespace Indigestion
{

    /// <summary>
    /// Orchestrates the process of ingesting data, searching for a predicted
    /// precipitation, and serializing the result to JSON.
    /// </summary>
    public class DigestionProcess
    {
	    private Ingester DataIngester;
		private Jester DataCollector;
		private Outgester JsonSerializer;

         /// <summary>
         /// This controls whether the ingestion process should occur
         /// if the file path passed as input matches this value, then
         /// no ingestion will occur.
         /// </summary>
		public string LastFilePath { get; set; }

        /// <summary>
        /// Default constructor. Each dependent component is constructed.
        /// </summary>
        public DigestionProcess()
        {
			DataCollector = new Jester();
			DataIngester = new Ingester(DataCollector, new FileContentImporter());
			JsonSerializer = new Outgester();
        }

        /// <summary>
        /// Constructor where each dependent component can be passed as input.
        /// This is useful in the unit tests.
        /// </summary>
        /// <param name="jester">Reference to the component which holds data and predicts precipitation</param>
        /// <param name="ingester">Reference to the component which ingests data</param>
        /// <param name="outgester">Reference to the component which serializes precipitation data to JSON</param>
        public DigestionProcess(Jester jester, Ingester ingester, Outgester outgester)
        {
            DataCollector = jester;
            DataIngester = ingester;
            JsonSerializer = outgester;
        }

        /// <summary>
        /// Main processing -
        ///    - if the file path differs from the LastFilePath, then data will be ingested
        ///    - using the input date, find the precipitation prediction
        ///    - serialize the precipitation prediction to JSON (if a prediction was found)
        ///    - if no data is ingested or if no prediction exists, then null is returned
        /// </summary>
        /// <param name="input">Date used to search for a precipitation prediction</param>
        /// <param name="filePath">Path to the file containing the input data</param>
        /// <returns>a string containing JSON representing a precipitation prediction</returns>
        public string process(DateTime input, string filePath)
		{
            if (!filePath.Equals(LastFilePath))
			{
				DataIngester.ingest(filePath);
				LastFilePath = filePath;
			}

			return JsonSerializer.serialize(DataCollector.predict(input));
		}
    }
}

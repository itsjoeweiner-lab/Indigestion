using System;

namespace Indigestion
{
    /// <summary>
    /// This class is responsible for ingesting data from a file and populating
    /// the data collection.
    /// </summary>
    public class Ingester
    {
		public Jester Collector { get; set; }
        public FileContentImporter Importer { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Ingester()
        {
            // NOP
        }

        /// <summary>
        /// Constructor where dependencies are passed as input
        /// </summary>
        /// <param name="collector">Reference to the class which holds the ingested precipitation data</param>
        /// <param name="importer">Reference to the class which imports data from a text file</param>
        public Ingester(Jester collector, FileContentImporter importer)
        {
			this.Collector = collector;
            this.Importer = importer;
        }

        /// <summary>
        /// Data is ingested from a text file found at the specified path
        /// The path can be relative or absolute.  All data ingested is stored
        /// in the data collection (Jester).  The data is assumed to be in CSV format.
        /// </summary>
        /// <param name="filePath">Absolute or relative path to the file containing input data</param>
        public virtual void ingest(string filePath)
		{
            // Assumes CSV file format; alternative is to use Excel InterOp lib
            string[] lines = Importer.import(filePath);

            foreach (string line in lines)
			{
                string[] data = line.Split(',');
                if (isValid(data))
				{
					Collector.addPrecipitationData(data[0],
                                                   data[1] + data[2], // hack
                                                   Convert.ToDouble(data[3]),
                                                   Convert.ToDouble(data[4]),
                                                   Convert.ToDouble(data[5]),
                                                   Convert.ToDateTime(data[6]),
                                                   Convert.ToDouble(data[7]));
				}
                
			}
			
		}

        /// <summary>
        /// Ensures that a line of data from the text file contains validly formatted data.
        /// </summary>
        /// <param name="data">An array of data values to be checked</param>
        /// <returns>true if all data is valid for the line; otherwise false</returns>
        public virtual bool isValid(string[] data)
		{
			if (data.Length != 8)
			{
				return false;
			}

            try
			{
                Convert.ToDouble(data[3]);
			}
            catch (Exception exc)
			{
				return false;
			}

            try
			{
				Convert.ToDouble(data[4]);
			}
            catch (Exception exc)
			{
				return false;
			}

            try
			{
				Convert.ToDouble(data[5]);
			}
            catch (Exception exc)
			{
				return false;
			}

            try
			{
				Convert.ToDateTime(data[6]);
			}
            catch (Exception exc)
			{
				return false;
			}

            try
			{
				Convert.ToDouble(data[7]);
			}
            catch (Exception exc)
			{
				return false;
			}

            return true;
		}
    }
}

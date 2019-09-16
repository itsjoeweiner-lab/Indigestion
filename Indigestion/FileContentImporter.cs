using System;

namespace Indigestion
{
    /// <summary>
    /// Class which encapsulates importing data directly from a text file.
    /// </summary>
    public class FileContentImporter
    {
        /// <summary>
        /// Imports data from a text file. Data is returned as an array
        /// of string.  Each array entry represents a "line" in the file.
        /// </summary>
        /// <param name="filePath">A path to the data file</param>
        /// <returns>An array of string where each entry is a line in the file</returns>
        public virtual string[] import(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath);
        }
    }
}

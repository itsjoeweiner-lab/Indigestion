using System;

namespace Indigestion
{
    /// <summary>
    /// Class containing the main method for running this application
    /// </summary>
    public class IndigestionApp
    {
        /// <summary>
        /// Main method for running this application
        /// Args:
        ///    1. absolute or relative path to the file containing input data
        ///    2. (optional) a date from which a precipitation prediction will be found
        /// If no date is provided, the current date will be used to find a precipitation prediction
        /// </summary>
        /// <param name="args">A list of string arguments to this appliction</param>
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("No args were provided.  2 args expected");
                return;
            }

            // args[0] - file path
            string filePath = args[0];

            // args[1] - date (optional)
            DateTime date;
            try
            {
                if (args.Length < 2 || args[1] == null)
                {
                    date = DateTime.Now;
                }
                else
                {
                    date = Convert.ToDateTime(args[1]);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Date time is incorrectly formatted {0}", args[1]);
                return;
            }

            string json = new DigestionProcess().process(date, filePath);
            Console.WriteLine(json);
            return;
        }
    }
}

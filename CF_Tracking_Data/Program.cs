using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// [{"id":4779,"bib":"2B001AC1000050000000B93F","scan_time":"2017-09-28T15:23:31.000Z","station":"Finish Line","race_id":4,"created_at":"2017-09-28T19:23:33.000Z","updated_at":"2017-09-28T19:23:33.000Z"}]

namespace CF_Tracking_Data
{
    class Program
    {

        

        static void Main(string[] args)
        {
            const int TOTAL_RIDERS = 50;
            int num65Riders = TOTAL_RIDERS * 15 / 100;      // 15% on 65 mile course
            int num30Riders = TOTAL_RIDERS * 35 / 100;      // 35% on 30 mile course
            int num12Riders = TOTAL_RIDERS / 2;             // 50% on 12 mile course

            const string DATA_FILE = "c:\\work\\cf_data_8.json";

            // Following IDs must match spreadsheet
            const string FINISH_SCANNER = "finish_1";
            const string SENIOR_SCANNER = "senior_1";
            const string LEGACY_SCANNER = "legacy_1";

            // Put all the scanned data into a list.  Once its all generated then
            // it gets sorted chronologically and dumped into a JSON file.
            // Riders riders = new Riders(TOTAL_RIDERS * 4);
            List<Rider> riderList = new List<Rider>(TOTAL_RIDERS * 4);

            Rider riderScan;
            DateTime timestamp;
            Random r = new Random();

            // Time windows taken from the "CFL Timing" spreadsheet
            // Windows are slightly modified so they do not overlap

            // 65 mile times
            DateTime timeStartLow = new DateTime(2017, 10, 7, 8, 30, 0);       // 8:30 - 8:35
            DateTime timeStartHigh = new DateTime(2017, 10, 7, 8, 35, 0);
            DateTime timeSeniorLow = new DateTime(2017, 10, 7, 9, 54, 0);       // 9:54 - 10:26
            DateTime timeSeniorHigh = new DateTime(2017, 10, 7, 10, 26, 0);
            DateTime timeLegacyLow = new DateTime(2017, 10, 7, 11, 23, 0);      // 11:23 - 12:27
            DateTime timeLegacyHigh = new DateTime(2017, 10, 7, 12, 27, 0);
            DateTime timeFinishLow = new DateTime(2017, 10, 7, 12, 28, 0);      // 12:28 - 1:25
            DateTime timeFinishHigh = new DateTime(2017, 10, 7, 13, 25, 0);

            for (int bib = 1; bib <= num65Riders; bib++)
            {
                // Start
                timestamp = RandomTime(r, timeStartLow, timeStartHigh);
                riderScan = new Rider(FormatBib(bib), FINISH_SCANNER, timestamp);
                riderList.Add(riderScan);

                // Senior Center
                timestamp = RandomTime(r, timeSeniorLow, timeSeniorHigh);
                riderScan = new Rider(FormatBib(bib), SENIOR_SCANNER, timestamp);
                riderList.Add(riderScan);

                // Legacy Farms
                timestamp = RandomTime(r, timeLegacyLow, timeLegacyHigh);
                riderScan = new Rider(FormatBib(bib), LEGACY_SCANNER, timestamp);
                riderList.Add(riderScan);
            
                // Finish
                timestamp = RandomTime(r, timeFinishLow, timeFinishHigh);
                riderScan = new Rider(FormatBib(bib), FINISH_SCANNER, timestamp);
                riderList.Add(riderScan);
            }

            // 30 mile times
            timeStartLow = new DateTime(2017, 10, 7, 10, 00, 0);        // 10:00 - 10:05
            timeStartHigh = new DateTime(2017, 10, 7, 10,  5, 0);
            timeSeniorLow = new DateTime(2017, 10, 7, 11, 32, 0);       // 11:32 - 12:00
            timeSeniorHigh = new DateTime(2017, 10, 7, 12, 00, 0);
            timeFinishLow = new DateTime(2017, 10, 7, 12, 01, 0);       // 11:52 - 12:41
            timeFinishHigh = new DateTime(2017, 10, 7, 12, 41, 0);

            for (int bib = num65Riders+1; bib <= num30Riders+num65Riders; bib++)
            {
                // Start
                timestamp = RandomTime(r, timeStartLow, timeStartHigh);
                riderScan = new Rider(FormatBib(bib), FINISH_SCANNER, timestamp);
                riderList.Add(riderScan);

                // Senior Center
                timestamp = RandomTime(r, timeSeniorLow, timeSeniorHigh);
                riderScan = new Rider(FormatBib(bib), SENIOR_SCANNER, timestamp);
                riderList.Add(riderScan);

                // Finish
                timestamp = RandomTime(r, timeFinishLow, timeFinishHigh);
                riderScan = new Rider(FormatBib(bib), FINISH_SCANNER, timestamp);
                riderList.Add(riderScan);
            }

            // 12 mile times
            timeStartLow = new DateTime(2017, 10, 7, 10, 0, 0);        // 10:00 - 10:05
            timeStartHigh = new DateTime(2017, 10, 7, 10, 5, 0);
            timeSeniorLow = new DateTime(2017, 10, 7, 10, 23, 0);       // 10:23 - 10:35
            timeSeniorHigh = new DateTime(2017, 10, 7, 10, 35, 0);
            timeFinishLow = new DateTime(2017, 10, 7, 11, 1, 0);        // 11:01 - 11:27
            timeFinishHigh = new DateTime(2017, 10, 7, 11, 27, 0);

            for (int bib = num30Riders+num65Riders+1; bib <= num12Riders+num30Riders+num65Riders; bib++)
            {
                // Start
                timestamp = RandomTime(r, timeStartLow, timeStartHigh);
                riderScan = new Rider(FormatBib(bib), FINISH_SCANNER, timestamp);
                riderList.Add(riderScan);

                // Senior Center
                timestamp = RandomTime(r, timeSeniorLow, timeSeniorHigh);
                riderScan = new Rider(FormatBib(bib), SENIOR_SCANNER, timestamp);
                riderList.Add(riderScan);

                // Finish
                timestamp = RandomTime(r, timeFinishLow, timeFinishHigh);
                riderScan = new Rider(FormatBib(bib), FINISH_SCANNER, timestamp);
                riderList.Add(riderScan);
            }

            // Convenient place for a breakpoint
            Console.WriteLine("Records generated: {0}", riderList.Count);

            //  Put data points into chronological order
            riderList.Sort((r1, r2) => DateTime.Compare(r1.ScanTime, r2.ScanTime));

            // Convert the data points to JSON format
            JsonHelper helper = new JsonHelper();
            // string result = helper.ConvertObjectToJson(riders);
            string result = helper.ConvertObjectToJson(riderList);

            // Dump the data to a file
            System.IO.File.WriteAllText(DATA_FILE, result);
         }

        static string FormatBib(int bib)
        {
            return string.Format("CFF000{0:D3}", bib);
        }
        // Generate a time in between the to limits to simulate a rider
        // stopping at a reporting point
        static DateTime RandomTime(Random r, DateTime timeLower, DateTime timeUpper)
        {
            // Get the difference between the two times
            TimeSpan timeDiff = timeUpper.Subtract(timeLower);

            // Create a random number that is within the number of seconds
            // of the upper and lower times
            int seconds = r.Next(Convert.ToInt32(timeDiff.TotalSeconds));

            // The generated time is the random number of seconds and the lower limit
            return timeLower.AddSeconds(seconds);
        }
    }
}

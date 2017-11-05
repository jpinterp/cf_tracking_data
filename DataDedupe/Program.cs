using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CF_Tracking_Data;

namespace DataDedupe
{
    class Program
    {
        // Open connection to SQL Server
        // Create empty record for every RFID tag.  This allows us to avoid determining
        //   if the row exists before updating.  When parsing the json, all of the 
        //   sql commands are updates instead of if insert fails, then update.
        // Parse the large json data file
        // For each json object, update the row in the SQL datatabase
        //
        static void Main(string[] args)
        {
            const int TOTAL_RIDERS = 400;
            const string TIMING_TABLE = "timing";
            const string DATA_FILE = "D:\\work\\cf_tracking_data\\cyclists_2010.json";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server = localhost\\SQLEXPRESS; Database = cf_tracking; Trusted_Connection = True;";
                conn.Open();

                // Only needs to be done once
                CreateEmptyRows(conn, TOTAL_RIDERS);

                // Read json file into a string 
                string jsonData = System.IO.File.ReadAllText(DATA_FILE);

                // Convert json into list of objects
                JsonHelper helper = new JsonHelper();
                List<Rider> riders = helper.ConvertJSonToObject<List<Rider>>(jsonData);
            
                foreach (Rider r in riders)
                {
                    UpdateRow(conn, r);
                }
            }
            
        }

        static void UpdateRow(SqlConnection conn, Rider r)
        {
            SqlCommand sqlCmd = new SqlCommand();   // keeps the compiler happy

            if (r.ScannerId.Equals("START"))
            {
                if (r.ScanTime.TimeOfDay <= new TimeSpan(10, 30, 0))
                {
                    sqlCmd = new SqlCommand("UPDATE timing SET start_time = @time WHERE bib = @bib");
                }
                else
                {
                    sqlCmd = new SqlCommand("UPDATE timing SET finish_time = @time WHERE bib = @bib");
                }
            }
            else if (r.ScannerId.Equals("SENIOR"))
            {
                sqlCmd = new SqlCommand("UPDATE timing SET senior_time = @time WHERE bib = @bib");
            }
            else if (r.ScannerId.Equals("LEGACY"))
            {
                sqlCmd = new SqlCommand("UPDATE timing SET legacy_time = @time WHERE bib = @bib");
            }

            sqlCmd.Connection = conn;
            sqlCmd.Parameters.Add(new SqlParameter("time", r.ScanTime));
            sqlCmd.Parameters.Add(new SqlParameter("bib", r.Bib));

            sqlCmd.ExecuteNonQuery();
        }


        static void CreateEmptyRows(SqlConnection conn, int maxRiders)
        {
            for (int i = 1; i <= maxRiders; i++)
            {
                string bib = FormatBib(i);

                SqlCommand insertCommand = new SqlCommand("INSERT INTO timing (bib) VALUES (@bibNumber)", conn);
                insertCommand.Parameters.Add(new SqlParameter("bibNumber", bib));
                insertCommand.ExecuteNonQuery();
            }
        }

        
        // Create a bib number in the format of CFF000xxx where xxx is the
        // number everyone sees that is displayed on the bib
        static string FormatBib(int bib)
        {
            return string.Format("CFF00{0:D3}", bib);
        }
    }
}

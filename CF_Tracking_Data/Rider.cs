using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

// [{"id":4779,
//   "bib":"2B001AC1000050000000B93F",
//   "scan_time":"2017-09-28T15:23:31.000Z",
//   "station":"Finish Line",
//   "race_id":4,
//   "created_at":"2017-09-28T19:23:33.000Z",
//   "updated_at":"2017-09-28T19:23:33.000Z"}]
//
namespace CF_Tracking_Data
{
    [DataContract]
    public class Rider
    {
        public Rider(string bib, string scannerId, DateTime  timestamp)
        {
            Bib = bib;
            ScannerId = scannerId;
            ScanTime = timestamp;
            CreateTime = timestamp;
            UpdateTime = timestamp;
            RaceId = "4";
        }

        [DataMember(Name ="bib")]
        public string Bib { get; set; }

        [DataMember(Name="scan_time")]    // JSON serializer sets the time format
        public DateTime ScanTime { get; set; }

        [DataMember(Name ="station")]
        public string ScannerId { get; set; }

        [DataMember(Name = "race_id")]
        public string RaceId { get; set; }

        [DataMember(Name = "created_at")]    // JSON serializer sets the time format
        public DateTime CreateTime { get; set; }

        [DataMember(Name = "updated_at")]    // JSON serializer sets the time format
        public DateTime UpdateTime { get; set; }


    }
}

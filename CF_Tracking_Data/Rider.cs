using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CF_Tracking_Data
{
    [DataContract]
    public class Rider
    {
        public Rider(int bib, string scannerId, DateTime  timestamp)
        {
            Bib = bib;
            RfId = bib;
            ScannerId = scannerId;
            Timestamp = timestamp;
        }

        [DataMember]
        public string ScannerId { get; set; }

        [DataMember]
        public int RfId { get; set; }

        [DataMember]
        public int Bib { get; set; }

        [DataMember]    // JSON serializer sets the time format
        public DateTime Timestamp { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace CF_Tracking_Data
{
    [DataContract]
    public class Riders
    {
        public Riders()
        {
            RiderList = new List<Rider>();
        }
        public Riders(int listSize)
        {
            RiderList = new List<Rider>(listSize);
        }

        [DataMember]
        public List<Rider> RiderList {get;}
    }
}

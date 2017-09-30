using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CF_Tracking_Data
{
    public class JsonHelper
    {
        public string ConvertObjectToJson<T>(T obj)
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            // settings.RootName = "riders";
            settings.DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("HH:mm:ss");  // HH for 24 hour clock

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T), settings);
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
    }
}

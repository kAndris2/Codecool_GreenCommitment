using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Common;

namespace Server
{
    public static class DataHandler
    {

        
        public static void Serialize(List<Measurement> measurments)
        {
            using (Stream fs = new FileStream("Measurments.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Measurement>));
                serializer.Serialize(fs, measurments);
            }
        }

        public static List<Measurement> DeSerialize()
        {
            List<Measurement> measurements = new List<Measurement>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Measurement>));
            using (FileStream fs = File.OpenRead("Measurments.xml"))
            {

                measurements = (List<Measurement>)serializer.Deserialize(fs);
            }
            return measurements;
        }


        public static void SaveToCSV(Measurement m)
        {
            string temp = $"{m.Id};{m.Value};{m.Type};{m.Time}\n";
            File.AppendAllText("Measurement.csv", temp);
        }
    }   
}

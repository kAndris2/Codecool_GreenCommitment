using System;

namespace Common
{
    public class Measurement
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public String Type { get; set; }
        public double Time { get; set; }

        public Measurement(int id, int value, string type, double time)
        {
            Id = id;
            Value = value;
            Type = type;
            Time = time;
        }

        public Measurement(string[] str)
        {
            Id = Convert.ToInt32(str[0]);
            Value = Convert.ToInt32(str[1]);
            Type = str[2];
            string iString = str[3];
            DateTime oDate = DateTime.ParseExact(iString, "yyyy-MM-dd HH:mm tt", null);
            //Time = Convert.ToInt64(str[3]);
            Time = oDate.ToUniversalTime().Subtract(
    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
    ).TotalMilliseconds;


        }

        public Measurement()
        { }

        public override string ToString()
        {
            double ticks = double.Parse($"{Time}");
            TimeSpan time = TimeSpan.FromMilliseconds(ticks);
            DateTime startdate = new DateTime(1970, 1, 1) + time;
            return $"ID: {Id}\n" +
                   $"Value: {Value}\n" +
                   $"Type: {Type}\n" +
                   $"Time: {startdate}";
        }
    }
}

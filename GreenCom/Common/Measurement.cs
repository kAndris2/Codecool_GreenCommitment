using System;

namespace Common
{
    public class Measurement
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public String Type { get; set; }
        public long Time { get; set; }

        public Measurement(int id, int value, string type, long time)
        {
            Id = id;
            Value = value;
            Type = type;
            Time = time;
        }

        public Measurement(string [] stringarr)
        {
            Id = int.Parse(stringarr[0]);
            Value = int.Parse(stringarr[1]); 
            Type = stringarr[2];
            Time = int.Parse(stringarr[3]);
        }

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

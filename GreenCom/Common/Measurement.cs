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

        public override string ToString()
        {
            return $"ID: {Id}\n" +
                   $"Value: {Value}\n" +
                   $"Type: {Type}\n" +
                   $"Time: {Time}";
        }
    }
}

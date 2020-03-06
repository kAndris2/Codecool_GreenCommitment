using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LiveCharts.Configurations;

namespace Graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cartesianChart1.Series = new SeriesCollection(Mappers.Xy<MyModel>()
                .X(m => (double)m.DateTime.Ticks / TimeSpan.FromMinutes(1).Ticks)
                .Y(m => m.Value))
            {
                new LineSeries
                {
                    Title = "This line represents Celsius degree values.",
                    Values = GetOPoints(GetCelsiusValues()),
                    PointGeometrySize = 15,
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "This line represents Water level values.",
                    Values = GetOPoints(GetWaterLevelValues()),
                    PointGeometrySize = 15,
                    LineSmoothness = 0
                },
                 new LineSeries
                {
                    Title = "This line represents Air pressure level values.",
                    Values = GetOPoints(GetAirPressureValues()),
                    PointGeometrySize = 15,
                    LineSmoothness = 0
                }
            };
            cartesianChart1.LegendLocation = LegendLocation.Right;
            cartesianChart1.AxisX.Add(new Axis
            {
                LabelFormatter = value => new DateTime((long)(value * TimeSpan.FromMinutes(1).Ticks)).ToString("t")
            });
        }

        private Dictionary<string, string> GetCelsiusValues()
        {
            var values = new Dictionary<string, string>();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] data = File.ReadAllLines(path + "/Graph/Measurement.csv");

            foreach (string item in data)
            {
                string[] temp = item.Split(';');
                if (temp.Contains("celsiusdegree"))
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (!values.ContainsKey(temp[3]))
                        {
                            values.Add(temp[3], temp[1]);
                        }
                    }

                }
            }
            return values;
        }
        private Dictionary<string, string> GetWaterLevelValues()
        {
            var values = new Dictionary<string, string>();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] data = File.ReadAllLines(path + "/Graph/Measurement.csv");

            foreach (string item in data)
            {
                string[] temp = item.Split(';');
                if (temp.Contains("waterlevel"))
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (!values.ContainsKey(temp[3]))
                        {
                            values.Add(temp[3], temp[1]);
                        }
                    }

                }
            }
            return values;
        }

        private Dictionary<string, string> GetAirPressureValues()
        {
            var values = new Dictionary<string, string>();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] data = File.ReadAllLines(path + "/Graph/Measurement.csv");

            foreach (string item in data)
            {
               
                
                    string[] temp = item.Split(';');
                if (temp.Contains("airpressure"))
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (!values.ContainsKey(temp[3]))
                        {
                            values.Add(temp[3], temp[1]);
                        }
                    }

                }
                   
                
            }
            return values;
        }

        public class MyModel : IComparable<MyModel>
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }

            public int CompareTo(MyModel other)
            {
                return DateTime.CompareTo(other.DateTime);
            }
        }


        private ChartValues<MyModel> GetOPoints(Dictionary<string, string> table)
        {
            List<MyModel> data = new List<MyModel>();
            

            foreach (KeyValuePair<string, string> item in table)
            {


                long temp_key = long.Parse(item.Key),
                    temp_value = Convert.ToInt64(item.Value);


                TimeSpan time = TimeSpan.FromMilliseconds(temp_key);
                DateTime startdate = new DateTime(1970, 1, 1) + time;

                data.Add(new MyModel { DateTime = startdate, Value = temp_value });
            }
            data.Sort();

            return new ChartValues<MyModel>(data);
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
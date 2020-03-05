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

namespace Graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Celsius:",
                    Values = GetOPoints(GetValues()),
                    PointGeometrySize = 15
                }
            };
        }

        private Dictionary<string, string> GetValues()
        {
            var values = new Dictionary<string, string>();
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] data = File.ReadAllLines(path + "/Graph/Measurement.csv");

            foreach (string item in data)
            {
                string[] temp = item.Split(';');
                for (int i = 0; i < temp.Length; i++)
                {
                    if (!values.ContainsKey(temp[1]))
                    {
                        values.Add(temp[1],temp[3]);
                    }
                }
            }
            return values;
        }
        
        private ChartValues<ObservablePoint> GetOPoints(Dictionary<string, string> table)
        {
            ChartValues<ObservablePoint> data = new ChartValues<ObservablePoint>();

            foreach (KeyValuePair<string, string> item in table)
            {
                long temp_key = int.Parse(item.Key),
                    temp_value = Convert.ToInt64(item.Value);
                data.Add(new ObservablePoint(temp_value, temp_key));
            }
            return data;
        }
    }
}
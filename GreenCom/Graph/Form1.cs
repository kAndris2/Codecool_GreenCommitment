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
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(1212, 20),
                        new ObservablePoint(1214, 80),
                        new ObservablePoint(1224, 200)
                    },
                    PointGeometrySize = 15
                }
            };
        }

        private Dictionary<int, string> GetValues()
        {
            var values = new Dictionary<int, string>();
            string path = @"C:\Users\andri\Desktop\Codecool\C#\Benti\Green_Commitment\GreenCommitment\GreenCom\RunProgram\bin\Debug\netcoreapp3.1\Measurement.csv";


            return values;
        }
    }
}
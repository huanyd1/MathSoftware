using LiveCharts;
using LiveCharts.Wpf;
using MathSoftware.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathSoftware.UCChart
{
    /// <summary>
    /// Interaction logic for UCScatterChart.xaml
    /// </summary>
    public partial class UCScatterChart : UserControl
    {
        private ScatterSeries _scatter;
        private ObjChartData _objChart;

        public UCScatterChart(ObjChartData objChart)
        {
            InitializeComponent();

            _objChart = objChart;
        }

        public ChartValues<double> _value;
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void UCScatterChart_Loaded(object sender, RoutedEventArgs e)
        {
            PointLabel = chartPoint =>
              string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            for (int i = 0; i < _objChart._lsColumn.Count; i++)
            {
                //Khởi tạo giá trị
                _scatter = new ScatterSeries();

                _value = new ChartValues<double>();

                for (int j = 0; j < _objChart._lsColumn[i].Length; j++)
                {
                    if (j == 0)
                    {
                        _scatter.Title = _objChart._lsColumn[i][j].ToString();
                        _scatter.Values = _value;
                        _scatter.DataLabels = true;
                        _scatter.LabelPoint = PointLabel;
                        _objChart._seriesCollection.Add(_scatter);

                        if (_objChart._titlePositon.ToString().Equals("Ở dưới"))
                        {
                            axisX.Position = AxisPosition.LeftBottom;
                        }
                        else
                        {
                            axisX.Position = AxisPosition.RightTop;
                        }

                        axisX.FontSize = 15;
                        axisX.Labels = _objChart._lsRow;
                        axisX.Title = _objChart._chartTitle.ToString();
                        //axisY.Title = _objChart._verticalAxis.ToString();

                        ScatterChart.LegendLocation = LegendLocation.Top;
                        ScatterChart.Series = _objChart._seriesCollection;
                    }
                    else
                    {
                        if (_objChart._lsColumn[i][j] == "")
                        {
                            _objChart._lsColumn[i][j] = "0";
                        }
                        else if (CheckIfAlphabet(_objChart._lsColumn[i][j]))
                        {
                            _value.Add(Convert.ToDouble(_objChart._lsColumn[i][j]));
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu chỉ được là kiểu số", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                }
            }
        }

        public bool CheckIfAlphabet(string salDesc)
        {
            Regex objAlphaPattern = new Regex(@"^[0-9.]*$");

            return objAlphaPattern.IsMatch(salDesc);

        }
    }
}

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
    /// Interaction logic for UCColumnChart.xaml
    /// </summary>
    public partial class UCColumnChart : UserControl
    {
        private ColumnSeries _column;
        private ObjChartData _objChart;
        public UCColumnChart(ObjChartData objChart)
        {
            InitializeComponent();

            _objChart = objChart;
        }

        public ChartValues<double> _value;
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void UCColumnChart_Loaded(object sender, RoutedEventArgs e)
        {
            PointLabel = chartPoint =>
              string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            for (int i = 0; i < _objChart._lsColumn.Count; i++)
            {
                //Khởi tạo giá trị
                _column = new ColumnSeries();

                _value = new ChartValues<double>();

                for (int j = 0; j < _objChart._lsColumn[i].Length; j++)
                {
                    if (j == 0)
                    {
                        _column.Title = _objChart._lsColumn[i][j].ToString();
                        _column.Values = _value;
                        _column.DataLabels = _objChart._showData;
                        _column.LabelPoint = PointLabel;
                        _objChart._seriesCollection.Add(_column);

                        if(_objChart._titlePositon.ToString().Equals("Ở dưới"))
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

                        if(_objChart._showNote == true)
                        {
                            axisY.Title = _objChart._verticalAxis.ToString();
                        }
                        else
                        {
                            axisY.Title = null;
                        }
                        
                        if(_objChart._noteUnitPosition.ToString().Equals("Bên trái"))
                        {
                            axisY.Position = AxisPosition.LeftBottom;
                        }
                        else
                        {
                            axisY.Position = AxisPosition.RightTop;
                        }

                        if (_objChart._noteUnitChart.ToString().Equals("Bên trái"))
                        {
                            ColumnChart.LegendLocation = LegendLocation.Left;
                        }
                        else if(_objChart._noteUnitChart.ToString().Equals("Bên phải"))
                        {
                            ColumnChart.LegendLocation = LegendLocation.Right;
                        }
                        else if (_objChart._noteUnitChart.ToString().Equals("Bên trên"))
                        {
                            ColumnChart.LegendLocation = LegendLocation.Top;
                        }
                        else
                        {
                            ColumnChart.LegendLocation = LegendLocation.Bottom;
                        }

                        ColumnChart.Series = _objChart._seriesCollection;
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

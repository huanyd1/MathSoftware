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
    /// Interaction logic for UCRowChart.xaml
    /// </summary>
    public partial class UCRowChart : UserControl
    {
        private RowSeries _row;
        private ObjChartData _objChart;

        public UCRowChart(ObjChartData objChart)
        {
            InitializeComponent();

            _objChart = objChart;
        }

        public ChartValues<double> _value;
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void UCRowChart_Loaded(object sender, RoutedEventArgs e)
        {
            PointLabel = chartPoint =>
              string.Format("{0} ({1:P})", chartPoint.X, chartPoint.Participation);

            for (int i = 0; i < _objChart._lsColumn.Count; i++)
            {
                //Khởi tạo giá trị
                _row = new RowSeries();

                _value = new ChartValues<double>();

                for (int j = 0; j < _objChart._lsColumn[i].Length; j++)
                {
                    if (j == 0)
                    {
                        _row.Title = _objChart._lsColumn[i][j].ToString();
                        _row.Values = _value;
                        _row.DataLabels = _objChart._showData;
                        _row.LabelPoint = PointLabel;
                        _objChart._seriesCollection.Add(_row);

                        if (_objChart._titlePositon.ToString().Equals("Ở dưới"))
                        {
                            axisX.Position = AxisPosition.LeftBottom;
                        }
                        else
                        {
                            axisX.Position = AxisPosition.RightTop;
                        }

                        axisY.FontSize = _objChart._titleSize;
                        axisY.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(_objChart._titleColor);
                        axisY.Labels = _objChart._lsRow;
                        axisY.Title = _objChart._chartTitle.ToString();

                        if (_objChart._showNote == true)
                        {
                            axisX.Title = _objChart._verticalAxis.ToString();
                        }
                        else
                        {
                            axisX.Title = null;
                        }

                        axisX.FontSize = _objChart._verticalSize;
                        axisX.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(_objChart._colorVerticalAxis);

                        if (_objChart._noteUnitPosition.ToString().Equals("Bên trái"))
                        {
                            axisY.Position = AxisPosition.LeftBottom;
                        }
                        else
                        {
                            axisY.Position = AxisPosition.RightTop;
                        }

                        if (_objChart._noteUnitChart.ToString().Equals("Bên trái"))
                        {
                            RowChart.LegendLocation = LegendLocation.Left;
                        }
                        else if (_objChart._noteUnitChart.ToString().Equals("Bên phải"))
                        {
                            RowChart.LegendLocation = LegendLocation.Right;
                        }
                        else if (_objChart._noteUnitChart.ToString().Equals("Bên trên"))
                        {
                            RowChart.LegendLocation = LegendLocation.Top;
                        }
                        else
                        {
                            RowChart.LegendLocation = LegendLocation.Bottom;
                        }

                        RowChart.Series = _objChart._seriesCollection;
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

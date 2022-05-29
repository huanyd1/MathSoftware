using LiveCharts.Wpf;
using MathSoftware.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MathSoftware
{
    public class GetChartData
    {
        private ObjChartData _objChartData;
        public GetChartData(ObjChartData objChartData)
        {
            _objChartData = objChartData;
        }

        public void GetRowData(DataTable _table)
        {
            try
            {
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    String row = _table.Rows[i][0].ToString();
                    _objChartData._lsRow.Add(row);
                }
                //Xóa ô dữ liệu đầu tiên
                _objChartData._lsRow.RemoveAt(0);
            }
            catch
            {
                MessageBox.Show(NotifyCommon.NotifyTable.TableRowError, NotifyCommon.NotifyType.TypeError, MessageBoxButton.OK);
            }
            
        }

        public void GetColumnData(DataTable _table)
        {
            try
            {
                for (int i = 0; i < _table.Rows.Count; i++)
                {
                    String[] column = new String[_table.Columns.Count];
                    for (int j = 0; j < _table.Columns.Count; j++)
                    {
                        column[j] = _table.Rows[i][j].ToString();
                    }
                    _objChartData._lsColumn.Add(column);
                }
            }
            catch
            {
                MessageBox.Show(NotifyCommon.NotifyTable.TableColumnError, NotifyCommon.NotifyType.TypeError, MessageBoxButton.OK);
            }
        }

        public void GetTitleChart(string title, int fontSize, string color)
        {
            string _title = "";
            int _fontSize = 25;
            string _color = "#000000";
            try
            {
                _title = title;
                _objChartData._chartTitle = _title;

                _fontSize = fontSize;
                _objChartData._titleSize = _fontSize;

                _color = color;
                _objChartData._titleColor = color;
            }
            catch
            {
                MessageBox.Show(NotifyCommon.NotifyTitle.TitleError, NotifyCommon.NotifyType.TypeError, MessageBoxButton.OK);
            }
        }

        public void GetChartType(ComboBox cbChartType)
        {
            string _chartType = "";
            try
            {
                _chartType = cbChartType.SelectedItem.ToString();
                _objChartData._chartType = _chartType;
            }
            catch
            {
                MessageBox.Show(NotifyCommon.NotifyChartType.TypeError, NotifyCommon.NotifyType.TypeError, MessageBoxButton.OK);
            }
        }

        public void GetNoteAxis(string axisX)
        {
            string _axisX = "";
            try
            {
                _axisX = axisX;

                _objChartData._verticalAxis = axisX;
            }
            catch
            {
                MessageBox.Show(NotifyCommon.NotifyAxis.AxisError, NotifyCommon.NotifyType.TypeError, MessageBoxButton.OK);
            }

        }

        public void GetShowValue(bool showNote, bool showData)
        {
            bool _isShowNote = true;
            bool _isShowData = true;
            try
            {
                _isShowNote = showNote;
                _isShowData = showData;

                _objChartData._showNote = _isShowNote;
                _objChartData._showData = _isShowData;
            }
            catch
            {
                MessageBox.Show(NotifyCommon.NotifyShowValue.ShowValueError, NotifyCommon.NotifyType.TypeError, MessageBoxButton.OK);
            }

        }

        public void GetPosition(string titlePosition, string unitPosition, string unitChart)
        {
            string _titlePosition = "";
            string _unitPosition = "";
            string _unitChart = "";

            try
            {
                _titlePosition = titlePosition.Replace("System.Windows.Controls.ListBoxItem: ", "");
                _unitPosition = unitPosition.Replace("System.Windows.Controls.ListBoxItem: ", "");
                _unitChart = unitChart.Replace("System.Windows.Controls.ListBoxItem: ", "");

                _objChartData._titlePositon = _titlePosition;
                _objChartData._noteUnitPosition = _unitPosition;
                _objChartData._noteUnitChart = _unitChart;
            }
            catch
            {
                MessageBox.Show(NotifyCommon.NotifyShowPosition.ShowPositionError, NotifyCommon.NotifyType.TypeError, MessageBoxButton.OK);
            }

        }
    }
}

using MathSoftware.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSoftware
{
    public class GetChartData
    {
        private ObjChartData _objChartData;
        public GetChartData(ObjChartData objChartData)
        {
            _objChartData = objChartData;
        }

        public void GetRowData(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                String row = table.Rows[i][0].ToString();
                _objChartData._lsRow.Add(row);
            }
            //Xóa ô dữ liệu đầu tiên
            _objChartData._lsRow.RemoveAt(0);
        }

        public void GetColumnData(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                String[] column = new String[table.Columns.Count];
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    column[j] = table.Rows[i][j].ToString();
                }
                _objChartData._lsColumn.Add(column);
            }
        }

        public void GetTitleChart(string title)
        {
            _objChartData._chartTitle = title;
        }

        public void GetChartType(string chartType)
        {
            _objChartData._chartType = chartType;
        }

        public void GetNoteAxis(string axisX, string axisY)
        {
            _objChartData._verticalAxis = axisX;
            _objChartData._horizontalAxis = axisY;
        }

        public void GetShowValue(bool showNote, bool showData)
        {
            _objChartData._showNote = showNote;
            _objChartData._showData = showData;
        }

        public void GetPosition(string titlePosition, string unitPosition, string unitChart)
        {
            _objChartData._titlePositon = titlePosition;
            _objChartData._noteUnitPosition = unitPosition;
            _objChartData._noteUnitChart = unitChart;
        }
    }
}

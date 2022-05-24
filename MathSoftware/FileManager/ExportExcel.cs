using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathSoftware.FileManager
{
    public class ExportExcel
    {
        private DataTable _dtRow;
        private DataTable _dtColumn;
        private DataRow _row;

        public ExportExcel(DataTable row, DataTable column)
        {
            _dtRow = row;
            _dtColumn = column;

            Export();
        }

        private void Export()
        {
            try
            {
                List<String> lines = new List<String>();

                string NameCol = "";

                int i = 0;

                foreach (DataRow dtR in _dtRow.Rows)
                {
                    int n = _dtRow.Rows.Count;

                    if (i == n - 1)
                    {
                        NameCol += dtR.ItemArray[0].ToString();
                    }
                    else
                    {
                        if (i != 0)
                        {
                            NameCol += dtR.ItemArray[0].ToString() + ",";
                        }
                        else
                        {
                            NameCol += ",";
                        }
                    }
                    i++;
                }
                lines.Add(NameCol);

                for (i = 0; i < _dtColumn.Rows.Count; i++)
                {
                    String[] strR = new String[_dtColumn.Columns.Count];
                    for (int j = 0; j < _dtColumn.Columns.Count; j++)
                    {
                        strR[j] = _dtColumn.Rows[i][j].ToString();
                    }

                    int n = strR.Length;
                    NameCol = "";
                    for (int j = 0; j < n; j++)
                    {
                        if (j == n - 1)
                        {
                            NameCol += strR[j].ToString();
                        }
                        else
                        {
                            NameCol += strR[j].ToString() + ",";
                        }
                    }
                    lines.Add(NameCol);
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV File|*.csv";
                saveFileDialog.Title = "Save an CSV File";
                saveFileDialog.ShowDialog();

                FileStream fs = (FileStream)saveFileDialog.OpenFile();

                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {

                    foreach (string line in lines)
                        writer.WriteLine(line);
                }
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

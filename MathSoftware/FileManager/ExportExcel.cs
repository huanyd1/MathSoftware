using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MathSoftware.FileManager
{
    public class ExportExcel
    {
        private DataTable _dtRow;
        private DataTable _dtColumn;

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

                string _nameColumn = "";

                int i = 0;

                foreach (DataRow _row in _dtRow.Rows)
                {
                    int n = _dtRow.Rows.Count;

                    if (i == n - 1)
                    {
                        _nameColumn += _row.ItemArray[0].ToString();
                    }
                    else
                    {
                        if (i != 0)
                        {
                            _nameColumn += _row.ItemArray[0].ToString() + ",";
                        }
                        else
                        {
                            _nameColumn += ",";
                        }
                    }
                    i++;
                }
                lines.Add(_nameColumn);

                for (i = 0; i < _dtColumn.Rows.Count; i++)
                {
                    String[] strR = new String[_dtColumn.Columns.Count];
                    for (int j = 0; j < _dtColumn.Columns.Count; j++)
                    {
                        strR[j] = _dtColumn.Rows[i][j].ToString();
                    }

                    int n = strR.Length;
                    _nameColumn = "";
                    for (int j = 0; j < n; j++)
                    {
                        if (j == n - 1)
                        {
                            _nameColumn += strR[j].ToString();
                        }
                        else
                        {
                            _nameColumn += strR[j].ToString() + ",";
                        }
                    }
                    lines.Add(_nameColumn);
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV File|*.csv|XLSX File|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.ShowDialog();

                string ext = Path.GetExtension(saveFileDialog.FileName);

                Mouse.OverrideCursor = Cursors.Wait;

                if (ext == ".xlsx" || ext == ".xls")
                {
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;
                    var package = new ExcelPackage(saveFileDialog.FileName);
                    ExcelWorksheet ws = package.Workbook.Worksheets.Add("Accounts");
                    ws.Cells["A1"].LoadFromDataTable(_dtColumn, true);
                    package.Save();
                }
                else
                {
                    FileStream fs = (FileStream)saveFileDialog.OpenFile();

                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {

                        foreach (string line in lines)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

                MessageBox.Show("Xuất dữ liệu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

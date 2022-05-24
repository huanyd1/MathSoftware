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

namespace MathSoftware.FileManager
{
    public class ImportExcel
    {
        private DataTable _dtRow;
        private DataTable _dtColumn;
        private DataRow _row;

        public ImportExcel(DataTable row, DataTable column)
        {
            _dtRow = row;
            _dtColumn = column;

            Import();
        }

        private void Import()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                string path = Environment.CurrentDirectory;
                openFileDialog.InitialDirectory = path;
                openFileDialog.ShowDialog();


                openFileDialog.Filter = "CSV File|*.csv|XLSX File|*.xlsx|XLS File|*.xls";
                string ext = Path.GetExtension(openFileDialog.FileName);
                if (ext == ".xlsx" || ext == ".xls")
                {
                    try
                    {
                        var package = new ExcelPackage(new FileInfo(openFileDialog.FileName));
                        ExcelPackage.LicenseContext = LicenseContext.Commercial;
                        ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                        _dtRow.Rows.Clear();
                        _dtColumn.Rows.Clear();
                        _row = _dtRow.NewRow();
                        _row[0] = "Nội dung biểu diễn";
                        _dtRow.Rows.Add(_row);
                        for (int i = workSheet.Dimension.Start.Column + 1; i <= workSheet.Dimension.End.Column; i++)
                        {
                            string nameCol = workSheet.Cells[1, i].Value.ToString();

                            _row = _dtRow.NewRow();
                            _row[0] = nameCol;
                            _dtRow.Rows.Add(_row);
                        }

                        for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                        {
                            int index = 0;
                            _row = _dtColumn.NewRow();
                            for (int j = workSheet.Dimension.Start.Column; j <= workSheet.Dimension.End.Column; j++)
                            {
                                string nameIndex = workSheet.Cells[i, j].Value.ToString();

                                _row[index++] = nameIndex;
                            }
                            _dtColumn.Rows.Add(_row);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi đọc file, vui lòng thử lại " + ex.ToString());
                    }
                }
                else if (ext == ".csv")
                {
                    _dtRow.Rows.Clear();

                    int k = 0;
                    string[] name = { "" };
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);

                    foreach (string line in lines)
                    {
                        if (lines[k] == lines[0])
                        {
                            name = line.Split(',');
                            for (int i = 0; i < name.Length; i++)
                            {
                                if (i == 0)
                                {
                                    name[i] = "Nội dung biểu diễn";
                                }
                                else
                                {
                                }
                                _row = _dtRow.NewRow();
                                _row[0] = name[i];
                                _dtRow.Rows.Add(_row);
                            }
                            k++;
                        }

                    }


                    _dtColumn.Rows.Clear();

                    foreach (string line in lines)
                    {
                        name = line.Split(',');
                        _row = _dtColumn.NewRow();
                        int index = 0;
                        for (int i = 0; i < name.Length; i++)
                        {
                            _row[index] = name[i];
                            index++;
                        }
                        _dtColumn.Rows.Add(_row);

                    }
                    _dtColumn.Rows.RemoveAt(0);
                }
                else
                {
                    MessageBox.Show("Sai định dạng dữ liệu,vui lòng thử lại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


            catch
            {
                System.Windows.MessageBox.Show("Bạn chưa chọn file hoặc sai định dạng, vui lòng thử lại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

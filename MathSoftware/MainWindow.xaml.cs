using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
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
using Aspose.Words;
using MaterialDesignThemes.Wpf;
using MathSoftware.FileManager;
using MathSoftware.Object;
using Microsoft.Win32;

namespace MathSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable dtRow;
        private DataTable dtColumn;
        private int _index = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //grdChart.Children.Add(new UCChart.UCPieChart());

            this.WindowState = WindowState.Maximized;
            this.Title = "Phần mềm toán học";
            this.Width = 1280;
            this.Height = 720;

            dtRow = new DataTable();
            dtRow.RowChanged += DtRow_RowChanged;
            dtColumn = new DataTable();

            LoadSettingTable();
            LoadcbChartType();
            LoadDataTable();
            dtgColumn.CanUserSortColumns = false;   
        }

        private void LoadSettingTable()
        {
            dtRow.DefaultView.AllowNew = false;
            dtColumn.DefaultView.AllowNew = false;

            DataColumn column = new DataColumn()
            {
                ColumnName = "Nội dung",
            };

            dtRow.Columns.Add(column);

            //dtgRow.ItemsSource = dtRow.DefaultView;

            dtgColumn.ItemsSource = dtColumn.DefaultView;
        }

        private void LoadDataTable()
        {
            DataRow row = dtRow.NewRow();
            row[0] = "Nội dung biểu diễn";
            dtRow.Rows.Add(row);

            row = dtRow.NewRow();
            row[0] = "Năm 1991";
            dtRow.Rows.Add(row);

            row = dtRow.NewRow();
            row[0] = "Năm 2000";
            dtRow.Rows.Add(row);

            row = dtRow.NewRow();
            row[0] = "Năm 2005";
            dtRow.Rows.Add(row);

            row = dtRow.NewRow();
            row[0] = "Năm 2013";
            dtRow.Rows.Add(row);

            RefreshTableColumn();

            row = dtColumn.NewRow();
            row[0] = "Khách quốc tế";
            row[1] = "0.3";
            row[2] = "2.1";
            row[3] = "3.5";
            row[4] = "7.5";
            dtColumn.Rows.Add(row);

            row = dtColumn.NewRow();
            row[0] = "Khách nội địa";
            row[1] = "1.5";
            row[2] = "11.2";
            row[3] = "16.0";
            row[4] = "35.0";
            dtColumn.Rows.Add(row);

            txtChartTitle.Text = "Số lượt khách quốc tế và khách nội địa ngành du lịch 1991 - 2003";
            txtNoteX.Text = "Triệu lượt khách";
        }

        private void LoadcbChartType()
        {
            cbChartType.Items.Add("Biểu đồ cột");
            cbChartType.Items.Add("Biểu đồ miền");
            cbChartType.Items.Add("Biểu đồ đường");
            cbChartType.Items.Add("Biểu đồ tròn");
            cbChartType.Items.Add("Biểu đồ điểm");
            cbChartType.Items.Add("Biểu đồ hàng");

            cbChartType.SelectedItem = "Biểu đồ cột";
        }

        private void RefreshTableColumn()
        {
            dtgColumn.ItemsSource = null;

            dtColumn.DefaultView.AllowNew = false;

            foreach (DataRow row in dtRow.Rows)
            {
                DataColumn column = new DataColumn
                {
                    ColumnName = row[0].ToString(),
                };
                dtColumn.Columns.Add(column);
            }

            dtgColumn.Items.Refresh();
            dtgColumn.ItemsSource = dtColumn.DefaultView;
        }

        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = dtRow.NewRow();
            row[0] = "Nội dung mới " + _index;
            _index++;
            dtRow.Rows.Add(row);

            dtgColumn.ItemsSource = null;

            DataColumn column = new DataColumn()
            {
                ColumnName = row[0].ToString(),
            };
            dtColumn.Columns.Add(column);

            dtgColumn.ItemsSource = dtColumn.DefaultView;
        }
        private void btnRemoveRow_Click(object sender, RoutedEventArgs e)
        {
            //if (dtgRow.SelectedItems.Count != 0)
            //{
            //    dtColumn.Columns.RemoveAt(dtgRow.SelectedIndex);
            //    dtgColumn.ItemsSource = null;

            //    dtRow.Rows.RemoveAt(dtgRow.SelectedIndex);

            //    dtgColumn.ItemsSource = dtColumn.DefaultView;

            //}
            //else
            //{
            //    MessageBox.Show("Bạn chưa chọn cột để xóa, vui lòng thử lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }

        private void DtRow_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            //dtgColumn.ItemsSource = null;
            //int index = dtgRow.SelectedIndex;

            //if (index != -1)
            //{
            //    DataRow row = dtRow.Rows[index];

            //    dtColumn.Columns[index].ColumnName = row[0].ToString();
            //    dtColumn.AcceptChanges();
            //    dtgColumn.ItemsSource = null;
            //    dtgColumn.ItemsSource = dtColumn.DefaultView;
            //}
            //dtgColumn.ItemsSource = dtColumn.DefaultView;
        }

        private void btnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = dtColumn.NewRow();
            row[0] = "Giá trị biểu diễn " + dtColumn.Rows.Count;
            dtColumn.Rows.Add(row);
        }

        private void btnRemoveColumn_Click(object sender, RoutedEventArgs e)
        {
            if (dtgColumn.SelectedItems.Count != 0)
            {
                dtColumn.Rows.RemoveAt(dtgColumn.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn cột để xóa, vui lòng thử lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private ObjChartData _objChart;
        public void GetAllChartData()
        {
            _objChart = new ObjChartData();

            GetChartData data = new GetChartData(_objChart);
            data.GetRowData(dtRow);
            data.GetColumnData(dtColumn);
            data.GetTitleChart(txtChartTitle.Text.ToString(), int.Parse(sldFsTittle.Value.ToString()), clpTittle.SelectedColorText.ToString());
            data.GetChartType(cbChartType);
            data.GetNoteAxis(txtNoteX.Text.ToString(), int.Parse(sldFsNote.Value.ToString()), clpNote.SelectedColorText.ToString());
            data.GetShowValue(bool.Parse(tgShowNote.IsChecked.ToString()), bool.Parse(tgShowValue.IsChecked.ToString()));
            data.GetPosition(lbTitleLocation.SelectedItem.ToString(), lbUnitLocation.SelectedItem.ToString(), lbUnitChart.SelectedItem.ToString());
        }

        private void ExecuteChart()
        {
            GetAllChartData();

            grdChart.Children.Clear();

            if (cbChartType.SelectedItem.ToString().Equals("Biểu đồ cột"))
            {
                grdChart.Children.Add(new UCChart.UCColumnChart(_objChart));
            }
            else if (cbChartType.SelectedItem.ToString().Equals("Biểu đồ tròn"))
            {
                grdChart.Children.Add(new UCChart.UCPieChart(_objChart));
            }
            else if (cbChartType.SelectedItem.ToString().Equals("Biểu đồ miền"))
            {
                grdChart.Children.Add(new UCChart.UCAreaChart(_objChart));
            }
            else if (cbChartType.SelectedItem.ToString().Equals("Biểu đồ đường"))
            {
                grdChart.Children.Add(new UCChart.UCLineChart(_objChart));
            }
            else if (cbChartType.SelectedItem.ToString().Equals("Biểu đồ hàng"))
            {
                grdChart.Children.Add(new UCChart.UCRowChart(_objChart));
            }
            else if (cbChartType.SelectedItem.ToString().Equals("Biểu đồ điểm"))
            {
                grdChart.Children.Add(new UCChart.UCScatterChart(_objChart));
            }
        }

        private void cbChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cbChartType.SelectedItem.ToString().Equals("Biểu đồ hàng"))
            //{
            //    tbTitleLocation.Text = "Vị trí Chú thích đơn vị tính:";
            //    tbUnitLocation.Text = "Vị trí Tiêu đề:";
            //}
            //else
            //{
            //    tbTitleLocation.Text = "Vị trí Tiêu đề:";
            //    tbUnitLocation.Text = "Vị trí Chú thích đơn vị tính:";
            //}
        }
        private void btnCreateChart_Click(object sender, RoutedEventArgs e)
        {
            ExecuteChart();
        }

        private void btnImportFile_Click(object sender, RoutedEventArgs e)
        {
            ImportExcel import = new ImportExcel(dtRow, dtColumn, dtgColumn);
        }

        private void btnExportFile_Click(object sender, RoutedEventArgs e)
        {
            ExportExcel export = new ExportExcel(dtRow, dtColumn);
        }

        private void cbShowNote_Click(object sender, RoutedEventArgs e)
        {
            ExecuteChart();
        }

        private void cbShowValue_Click(object sender, RoutedEventArgs e)
        {
            ExecuteChart();
        }

        private void sldFsTittle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if(_objChart != null)
            //{
            //    int _size = int.Parse(sldFsTittle.Value.ToString());
            //    _objChart._titleSize = _size;
            //    ExecuteChart();
            //}
        }

        private void sldFsNote_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (_objChart != null)
            //{
            //    int _size = int.Parse(sldFsTittle.Value.ToString());
            //    _objChart._titleSize = _size;
            //    ExecuteChart();
            //}
        }

        private void clpTittle_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            //string _color = "#000000";
            //if (_objChart != null)
            //{
            //    _color = clpTittle.SelectedColorText.ToString();
            //    _objChart._titleColor = _color;
            //    ExecuteChart();
            //}
        }

        private void clpNote_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            //string _color = "#000000";
            //if (_objChart != null)
            //{
            //    _color = clpNote.SelectedColorText.ToString();
            //    _objChart._colorVerticalAxis = _color;
            //    ExecuteChart();
            //}
        }

        private void lbTitleLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_objChart != null)
            {
                ExecuteChart();
            }
        }

        private void lbUnitLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_objChart != null)
            {
                ExecuteChart();
            }
        }

        private void btnExportChart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PNG File| *.png | JPG File | *.jpg";
                save.Title = "Save chart to Image";
                save.ShowDialog();

                Mouse.OverrideCursor = Cursors.Wait;

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)grdChart.ActualWidth, (int)grdChart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(grdChart);

                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                MemoryStream stream = new MemoryStream();
                png.Save(stream);
                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                image.Save(save.FileName);

                Mouse.OverrideCursor = Cursors.Arrow;
                MessageBox.Show("Lưu biểu đồ thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                Mouse.OverrideCursor = Cursors.Arrow;

                MessageBox.Show("Đã xảy ra lỗi trong quá trình lưu biểu đồ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExportPDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF File| *.pdf";
                save.Title = "Save chart to PDF";
                save.ShowDialog();

                Mouse.OverrideCursor = Cursors.Wait;

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)grdChart.ActualWidth, (int)grdChart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(grdChart);

                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                MemoryStream stream = new MemoryStream();
                png.Save(stream);
                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

                string pathImage = string.Format("{0}\\{1}", Environment.CurrentDirectory, "chart" + DateTime.Now.ToString("ddmmyyyyhhmms") + ".png");

                image.Save(pathImage);

                var doc = new Document();
                var builder = new DocumentBuilder(doc);

                builder.InsertImage(pathImage);

                doc.Save(save.FileName);

                Mouse.OverrideCursor = Cursors.Arrow;
                MessageBox.Show("Lưu biểu đồ thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                Mouse.OverrideCursor = Cursors.Arrow;

                MessageBox.Show("Đã xảy ra lỗi trong quá trình lưu biểu đồ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tgShowNote_Click(object sender, RoutedEventArgs e)
        {
            if(tgShowNote.IsChecked == true)
            {
                iconCheck.Kind = PackIconKind.CheckboxMarkedCircleOutline;
                
            }
            else
            {
                iconCheck.Kind = PackIconKind.CheckboxBlankCircleOutline;
            }
        }

        private void tgShowValue_Click(object sender, RoutedEventArgs e)
        {
            if (tgShowValue.IsChecked == true)
            {
                iconCheckValue.Kind = PackIconKind.CheckboxMarkedCircleOutline;

            }
            else
            {
                iconCheckValue.Kind = PackIconKind.CheckboxBlankCircleOutline;
            }
        }
    }
}

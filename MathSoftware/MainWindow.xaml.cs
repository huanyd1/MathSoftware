using System;
using System.Collections.Generic;
using System.Data;
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
using MathSoftware.FileManager;
using MathSoftware.Object;

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

            dtgRow.ItemsSource = dtRow.DefaultView;

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
            if (dtgRow.SelectedItems.Count != 0)
            {
                dtColumn.Columns.RemoveAt(dtgRow.SelectedIndex);
                dtgColumn.ItemsSource = null;

                dtRow.Rows.RemoveAt(dtgRow.SelectedIndex);

                dtgColumn.ItemsSource = dtColumn.DefaultView;

            }
            else
            {
                MessageBox.Show("Bạn chưa chọn cột để xóa, vui lòng thử lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DtRow_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            dtgColumn.ItemsSource = null;
            int index = dtgRow.SelectedIndex;

            if (index != -1)
            {
                DataRow row = dtRow.Rows[index];

                dtColumn.Columns[index].ColumnName = row[0].ToString();
                dtColumn.AcceptChanges();
                dtgColumn.ItemsSource = null;
                dtgColumn.ItemsSource = dtColumn.DefaultView;
            }
            dtgColumn.ItemsSource = dtColumn.DefaultView;
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
            data.GetTitleChart(txtChartTitle.Text.ToString());
            data.GetChartType(cbChartType);
            data.GetNoteAxis(txtNoteX.Text.ToString());
            data.GetShowValue(bool.Parse(cbShowNote.IsChecked.ToString()), bool.Parse(cbShowValue.IsChecked.ToString()));
            data.GetPosition(lbTitleLocation.SelectedItem.ToString(), lbUnitLocation.SelectedItem.ToString(), lbUnitChart.SelectedItem.ToString());
        }

        private void btnCreateChart_Click(object sender, RoutedEventArgs e)
        {
            GetAllChartData();

            grdChart.Children.Clear();

            if(cbChartType.SelectedItem.ToString().Equals("Biểu đồ cột"))
            {
                grdChart.Children.Add(new UCChart.UCColumnChart(_objChart));
            }
            else if(cbChartType.SelectedItem.ToString().Equals("Biểu đồ tròn"))
            {
                grdChart.Children.Add(new UCChart.UCPieChart(_objChart));
            }
            else if(cbChartType.SelectedItem.ToString().Equals("Biểu đồ miền"))
            {
                grdChart.Children.Add(new UCChart.UCAreaChart(_objChart));
            }
            else if(cbChartType.SelectedItem.ToString().Equals("Biểu đồ đường"))
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

        private void btnImportFile_Click(object sender, RoutedEventArgs e)
        {
            ImportExcel import = new ImportExcel(dtRow, dtColumn, dtgColumn);
        }

        private void btnExportFile_Click(object sender, RoutedEventArgs e)
        {
            ExportExcel export = new ExportExcel(dtRow, dtColumn);
        }
    }
}

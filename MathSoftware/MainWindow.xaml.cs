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
            this.WindowState = WindowState.Maximized;
            this.Title = "Phần mềm toán học";
            this.Width = 1280;
            this.Height = 720;

            dtRow = new DataTable();
            dtRow.RowChanged += DtRow_RowChanged;
            dtColumn = new DataTable();

            DataColumn column = new DataColumn()
            {
                ColumnName = "Nội dung biểu diễn",
            };
            dtColumn.Columns.Add(column);

            LoadDataTable();
        }

        private void LoadDataTable()
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
                dtColumn.Columns.RemoveAt(dtgRow.SelectedIndex + 1);
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
    }
}

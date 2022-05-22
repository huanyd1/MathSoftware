using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSoftware.NotifyCommon
{
    public static class NotifyCommon
    {
        public class NotifyType
        {
            public const string TypeSuccess = "Thành công";
            public const string TypeError = "Thất bại";
            public const string TypeWarning = "Cảnh báo";
        }
        public class NotifyTable
        {
            public const string TableRowError = "Có lỗi xử lý dữ liệu bảng 'Tiêu đề', xin vui lòng thử lại!";
            public const string TableColumnError = "Có lỗi xử lý dữ liệu bảng 'Dữ liệu', xin vui lòng thử lại!";
        }
        public class NotifyTitle
        {
            public const string TitleError = "Tên biểu đồ không đúng định dạng, vui lòng thử lại!";
        }
        public class NotifyChartType
        {
            public const string TypeError = "Không thể đọc được dữ liệu loại biểu đồ, vui lòng thử lại!";
        }
        public class NotifyAxis
        {
            public const string AxisError = "Không thể đọc được dữ liệu chú thích thanh, vui lòng thử lại!";
        }
        public class NotifyShowValue
        {
            public const string ShowValueError = "Không thể đọc được dữ liệu hiện dữ liệu biểu đồ, vui lòng thử lại!";
        }
        public class NotifyShowPosition
        {
            public const string ShowPositionError = "Không thể đọc được dữ liệu hiện dữ liệu vị trí, vui lòng thử lại!";
        }
    }
}

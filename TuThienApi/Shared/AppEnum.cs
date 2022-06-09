using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared
{
    public class AppEnum
    {
        #region Status
        public enum ProjectStatus
        {
            All = 0,
            [Description("Đang chờ")]
            Wait = 1,
            [Description("Đang thực thi")]
            Implementation = 2,
            [Description("Đã hoàn thành")]
            Complete = 3,
            [Description("Dự án đã hủy")]
            Cancelled = 4
        }

        public enum ProcessStatus
        {
            All = 0,
            [Description("Chưa bắt đầu")]
            NotStarted = 1,
            [Description("Đang chờ")]
            Waiting = 2,
            [Description("Đã thực hiện")]
            Implemented = 3
        }

        public enum UserStatus
        {
            All = 0,
            [Description("Khóa")]
            Lock = 1,
            [Description("Hoạt động")]
            UnLock = 2
        }

        public enum NewsStatus
        {
            All = 0,
            [Description("Ẩn")]
            Hide = 1,
            [Description("Hiện")]
            Show = 2
        }

        public enum ReclaimStatus
        {
            All = 0,
            [Description("Chờ")]
            Wait = 1,
            [Description("Hoàn thành")]
            Complete = 2
        }
        #endregion

        #region Type
        public enum UserType
        {
            All = 0,
            [Description("Cá nhân")]
            Person = 1,
            [Description("Tổ chức")]
            Organization = 2
        }

        public enum TransactionType
        {
            All = 0,
            [Description("Đóng góp")]
            Donate = 1,
            [Description("Hoàn tiền")]
            Refund = 2,
            [Description("Rút tiền")]
            WithDraw = 3
        }
        #endregion
    }
}

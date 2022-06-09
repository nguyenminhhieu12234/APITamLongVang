using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.ResponeModels
{
    public class AppRespone<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }

        public static AppRespone<T> Failed(string Message = "Có lỗi xảy ra!", object Data = null, int Total = 0)
        {
            return new AppRespone<T> { IsSuccess = false, Message = Message, Data = (T)Data, Total = Total };
        }

        public static AppRespone<T> Success(object Data = null, string Message = "Thành công!", int Total = 0)
        {
            return new AppRespone<T> { IsSuccess = true, Message = Message, Data = (T)Data, Total = Total };
        }
    }
}

using TuThienApi.Shared.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TuThienApi.Shared.Extensions
{
    public static class AppExtensions
    {
        public static string RemoveUnicode(this string input)
        {
            if (input == null) return "";
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public static string ToLinuxPath(this string str)
        {
            return str.Replace('\\', '/');
        }

        /// <summary>
        /// Replace string with replacement value if it is null
        /// </summary>
        /// <param name="str"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceNull(this string str, string replacement = "")
        {
            if (str == null) str = "";

            //Nếu str là null
            if (string.IsNullOrEmpty(str))
            {
                //Nếu có string thay thế thì mới thay thế
                if (!string.IsNullOrEmpty(replacement))
                {
                    str = replacement;
                }
            }

            return str;
        }

        /// <summary>
        /// Determine whether this string is null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string RelativePath(this string path)
        {
            LogServices.WriteInfo(path);
            String[] separator = { "wwwroot\\" };
            return path.Split(separator, StringSplitOptions.None)[1];
        }

        private static CultureInfo vnCuture = new CultureInfo("vi-VN");

        public static string TimeAgo(this DateTime dateTime)
        {

            string result;

            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} giây trước", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    string.Format("Khoảng {0} phút trước", timeSpan.Minutes) : "Khoảng 1 phút trước";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    string.Format("Khoảng {0} giờ trước", timeSpan.Hours) : "Khoảng 1 giờ trước";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    string.Format("Khoảng {0} ngày trước", timeSpan.Days) : "Hôm qua";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    string.Format("Khoảng {0} tháng trước", timeSpan.Days / 30) : "Khoảng 1 tháng trước";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    string.Format("Khoảng {0} năm trước", timeSpan.Days / 365) : "Khoảng 1 năm trước";
            }

            return result;
        }

        public static DateTime ToDateTime(this string date)
        {
            DateTime now = DateTime.Now;
            if (!string.IsNullOrEmpty(date))
            {
                if (DateTime.TryParse(date, vnCuture, DateTimeStyles.None, out DateTime dOutDate))
                {
                    return dOutDate;
                }
                else
                {
                    throw new Exception($"Ngày tháng không đúng định dạng");
                }
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static DateTime StartOfWeek(this DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static bool Beetween(this DateTime date, string fromDate, string toDate)
        {
            if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
            {
                return true;
            }

            DateTime dFromDate = (DateTime)fromDate.ToDateTime();
            DateTime dToDate = (DateTime)toDate.ToDateTime();

            return date.Date >= dFromDate.Date && date.Date <= dToDate.Date;
        }

        public static bool Beetween(this DateTime date, DateTime fromDate, DateTime toDate)
        {
            return date >= fromDate && date <= toDate;
        }
        public static bool Like(this string str, string keyword)
        {
            if (!keyword.IsNullOrEmpty())
            {
                keyword = Regex.Replace(keyword, @"[\,\.\-\$\#\@\!\(\)\,]", "");
            }
            keyword = keyword == null ? "" : keyword;
            keyword = keyword.ToLower().Replace(" ", "");
            string newstr = str.RemoveUnicode().Trim().ToLower().Replace(" ", "");
            bool a = newstr.Contains(keyword.RemoveUnicode().Trim())
                || keyword.IsNullOrEmpty();
            return a;
        }
        //public static bool Like(this string str, string keyword)
        //{
        //    keyword = keyword == null ? "" : keyword;
        //    keyword = keyword.ToLower().Replace(" ","");
        //    string newstr = str.RemoveUnicode().Trim().ToLower().Replace(" ", "");
        //    bool a= newstr.Contains(keyword.RemoveUnicode().Trim())
        //        || keyword.IsNullOrEmpty();
        //    return a;
        //}

        public static IList<T> Pagging<T>(this IList<T> src, int pageItem, int pageIndex){
            int index = pageIndex - 1;
            index = index < 0 ? 0 : index;

            int skip = index * pageItem;
            int take = pageItem;

            src = src.Skip(skip).Take(take).ToList();
            return src;
        }
        public static IQueryable<T> PaggingIQuery<T>(this IQueryable<T> src, int pageItem, int pageIndex)
        {
            int index = pageIndex - 1;
            index = index < 0 ? 0 : index;

            int skip = index * pageItem;
            int take = pageItem;

            src = src.Skip(skip).Take(take);

            return src;
        }
    }
}

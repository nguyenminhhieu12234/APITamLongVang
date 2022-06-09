using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TuThienApi.Shared.Extensions
{
    public class AppGeneral
    {
        public string TypeFileUpload(string fileName)
        {
            Regex regex = new Regex(@"^|\.+.*");
            MatchCollection result = regex.Matches(fileName);
            return result[1].ToString();
        }
    }
}

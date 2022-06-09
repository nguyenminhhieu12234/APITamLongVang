using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.Extensions
{
    public static class LogServices
    {
        public static void WriteInfo(string msg)
        {
            DateTime now = DateTime.Now;
            string content = $"info: [{now.ToString("HH:mm dd/MM/yyyy")}] {msg}";
            Console.WriteLine(content);
        }

        public static void WriteError(string msg)
        {
            DateTime now = DateTime.Now;
            string content = $"error: [{now.ToString("HH:mm dd/MM/yyyy")}] {msg}";
            Console.WriteLine(content);
        }
    }
}

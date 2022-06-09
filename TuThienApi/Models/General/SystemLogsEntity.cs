using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Models.General
{
    public class SystemLogsEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string RequestType { get; set; }
        public string RequestBody { get; set; }
        public string QueryString { get; set; }
        public string Status { get; set; }
        public string ResponeTime { get; set; }
        public string ErrorMessage { get; set; }
    }
}

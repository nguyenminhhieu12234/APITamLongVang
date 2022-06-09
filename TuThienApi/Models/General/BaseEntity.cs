using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Models.General
{
    public class BaseEntity
    {
        public int CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.ResponeModels.Dashboard
{
    public class ObjectRating
    {
        public ObjectRating(string FullName, decimal Value)
        {
            FullName = FullName;
            Value = Value;
        }

        public string FullName { get; set; }
        public decimal Value { get; set; }
    }
}

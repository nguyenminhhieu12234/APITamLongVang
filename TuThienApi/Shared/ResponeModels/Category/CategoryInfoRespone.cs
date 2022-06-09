using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Shared.RequestModels.Project;

namespace TuThienApi.Shared.ResponeModels.Category
{
    public class CategoryInfoRespone
    {
        public CategoryInfoRespone(CategoryEntity category)
        {
            CategoryName = category.CategoryName;
            Type = category.Type;
            Icon = category.File.FilePath;
        }

        public string CategoryName { get; set; }
        public int Type { get; set; }
        public string Icon { get; set; }
    }
}

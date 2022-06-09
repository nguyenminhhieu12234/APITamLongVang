using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.General;

namespace TuThienApi.Shared.ResponeModels.Category
{
    public class CategoryRespone
    {
        private readonly ApplicationDbContext _DbContext;

        public CategoryRespone(CategoryEntity cateogry, ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            CategoryId = cateogry.Id;
            CategoryName = cateogry.CategoryName;
            Icon = _DbContext.Files.Where(x => x.CategoryId == cateogry.Id).Select(x => x.FilePath).SingleOrDefault();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Icon { get; set; }
    }
}

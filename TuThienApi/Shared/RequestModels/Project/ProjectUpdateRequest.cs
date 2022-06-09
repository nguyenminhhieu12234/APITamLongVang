using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.RequestModels.Project
{
    public class ProjectUpdateRequest
    {
        #region Project data
        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        public string FriendlyUrl { get; set; }
        public string Summary { get; set; }
        public string ProblemToAddress { get; set; }
        public string Solution { get; set; }
        public string Location { get; set; }
        public string Impact { get; set; }
        public string AddressContract { get; set; }
        #endregion

        public FileRequest Banner { get; set; }
        public List<ProjectCategoryRequest> Category { get; set; }
    }
}

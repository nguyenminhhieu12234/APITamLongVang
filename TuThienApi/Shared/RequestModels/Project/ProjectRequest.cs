using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Shared.RequestModels.Project;
using static TuThienApi.Models.General.ReclaimEntity;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.Models
{
    public class ProjectRequest
    {
        #region Project data
        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Wait;

        public string FriendlyUrl { get; set; }
        public string Summary { get; set; }
        public string ProblemToAddress { get; set; }
        public string Solution { get; set; }
        public string Location { get; set; }
        public string Impact { get; set; }
        public DateTime EndDate { get; set; }
        public string AddressContract { get; set; }
        public decimal AmountNeed { get; set; }
        public decimal AmountTRXNeed { get; set; }
        #endregion

        public IList<ProcessRequest> Process { get; set; }
        public IList<ProjectCategoryRequest> Category { get; set; }
        public FileRequest Banner { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Project;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.Global
{
    public class UpdateStatusProject
    {
        private ApplicationDbContext _DbContext;

        public UpdateStatusProject(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            var listProject = _DbContext.Projects.Where(delegate (ProjectEntity x) {
                return (x.Status == ProjectStatus.Implementation)
                && (x.EndDate.Date == DateTime.Today.Date)
                || (x.EndDate.Date < DateTime.Today.Date);
            }).ToList();
            foreach(var project in listProject)
            {
                if(project.AmountNow >= project.AmountTRXNeed)
                {
                    project.Status = ProjectStatus.Complete;
                }
                else if(project.AmountNow < project.AmountTRXNeed)
                {
                    project.Status = ProjectStatus.Cancelled;
                }
            }

            _DbContext.SaveChanges();
        }
    }
}

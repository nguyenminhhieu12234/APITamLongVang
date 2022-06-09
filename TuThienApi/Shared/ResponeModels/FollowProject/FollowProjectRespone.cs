using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Project;
using TuThienApi.Models.Users;
using TuThienApi.Shared.ResponeModels.Projects;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.ResponeModels.FollowProject
{
    public class FollowProjectRespone
    {
        private readonly ApplicationDbContext _DbContext;

        public FollowProjectRespone(FavoriteProjectEntity followproject, ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            ProjectFollow = _DbContext.Projects
                .Include(x => x.User)
                .Include(x => x.File)
                .Where(x => x.Id == followproject.ProjectId)
                .Select(x => new ProjectRespone(x))
                .SingleOrDefault();
        }

        public ProjectRespone ProjectFollow { get; set; }
    }
}

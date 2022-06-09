using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.General;
using TuThienApi.Models.Project;
using TuThienApi.Shared.ResponeModels.Category;
using TuThienApi.Shared.ResponeModels.Files;
using TuThienApi.Shared.ResponeModels.TransactionHistory;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.ResponeModels.Projects
{
    public class DetailProjectRespone
    {
        private readonly ApplicationDbContext _DbContext;

        public DetailProjectRespone(ProjectEntity p, ApplicationDbContext DbContext, int CurrentUser)
        {
            _DbContext = DbContext;
            Title = p.Title;
            ShortDescription = p.ShortDescription;
            AddressContract = p.AddressContract;
            Category = p.ProjectCategories.Select(c => c.Category).ToList().Select(c => new CategoryRespone(c, _DbContext)).ToList();
            //UserCreate
            UserCreateId = p.CreateUser;
            UserCreate = p.User.FullName;
            CreateTime = p.CreateTime;
            FriendlyUrl = p.FriendlyUrl;
            AmountNow = p.AmountNow;
            AmountNeed = p.AmountNeed;
            AmountTRXNeed = p.AmountTRXNeed;
            Status = p.Status;
            Summary = p.Summary;
            ProblemToAddress = p.ProblemToAddress;
            Solution = p.Solution;
            Location = p.Location;
            Impact = p.Impact;
            EndDate = p.EndDate;
            UserType = _DbContext.Users.Where(x => x.Id == p.User.Id).Select(x => x.Type).SingleOrDefault();
            BannerPath = _DbContext.Files.Where(f => f.Id == p.FileId).Select(f => f.FilePath).SingleOrDefault();

            Processes = p.Processes.Select(process => new ProcessRespone(process, _DbContext)).ToList();

            Articals = _DbContext.Articals.Include(x => x.File).Where(x => x.ProjectId == p.Id).Select(x => new ArticalRespone(x, _DbContext)).ToList();

            if (CurrentUser == p.CreateUser)
            {
                IsEdit = true;
            }
            else
            {
                IsEdit = false;
            }

            var follow = _DbContext.FavoriteProjectEntities.Where(x => x.ProjectId == p.Id && x.UserId == CurrentUser && x.IsDeleted == false).SingleOrDefault();

            if (follow != null)
                IsFollow = true;
            else
                IsFollow = false;

            Transaction = _DbContext.TransactionHistoryEntity.Where(x => x.ProjectId == p.Id).Select(x => new TransactionRespone(x, _DbContext)).ToList();
        }

        public int UserCreateId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string AddressContract { get; set; }
        public string FriendlyUrl { get; set; }
        public List<CategoryRespone> Category { get; set; }
        public string UserCreate { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal AmountNow { get; set; }
        public decimal AmountNeed { get; set; }
        public decimal AmountTRXNeed { get; set; }
        public ProjectStatus Status { get; set; }
        public string Summary { get; set; }
        public string ProblemToAddress { get; set; }
        public string Solution { get; set; }
        public string Location { get; set; }
        public string Impact { get; set; }
        public DateTime EndDate { get; set; }
        public UserType UserType { get; set; }
        public string? BannerPath { get; set; }
        public bool IsEdit { get; set; }
        public bool IsFollow { get; set; }

        public List<ProcessRespone> Processes { get; set; }
        public List<FileResponse> Files { get; set; }

        public List<ArticalRespone> Articals { get; set; }
        public List<TransactionRespone> Transaction { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Project;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.ResponeModels.Projects
{
    public class ProjectRespone
    {
        public ProjectRespone(ProjectEntity p)
        {
            Id = p.Id;
            Title = p.Title;
            ShortDescription = p.ShortDescription;
            AddressContract = p.AddressContract;
            //Category
            //Category = p.ProjectCategories.Select(c => c.Category).ToList().FirstOrDefault().CategoryName;
            //User
            UserCreate = p.User.FullName;
            FriendlyUrl = p.FriendlyUrl;
            CreateTime = p.CreateTime;
            AmountNow = p.AmountNow;
            AmountNeed = p.AmountNeed;
            AmountTRXNeed = p.AmountTRXNeed;
            Status = p.Status;
            //Summary = p.Summary;
            Solution = p.Solution;
            //ProblemToAddress = p.ProblemToAddress;
            Impact = p.Impact;
            EndDate = p.EndDate;
            BannerPath = p.File.FilePath;
        }

        public int Id { get; set; }
        public string Title { get; set; } = null;
        public string ShortDescription { get; set; }
        public string AddressContract { get; set; }
        //public string Category { get; set; }
        public string UserCreate { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal AmountNow { get; set; }
        public decimal AmountNeed { get; set; }
        public ProjectStatus Status { get; set; }
        //public string Summary { get; set; }
        //public string ProblemToAddress { get; set; }
        public string Solution { get; set; }
        public string Impact { get; set; }
        public DateTime EndDate { get; set; }
        public UserType UserType { get; set; }
        public string BannerPath { get; set; }
        public string FriendlyUrl { get; set; }
        public decimal AmountTRXNeed { get; set; }
    }
}

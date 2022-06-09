using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Models.Users;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Models.Project
{
    public class ProjectEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Wait;
        public string FriendlyUrl { get; set; }
        public string Summary { get; set; }
        public string ProblemToAddress { get; set; }
        public string Solution { get; set; }
        public string Location { get; set; }
        public string Impact { get; set; }
        public DateTime EndDate { get; set; }
        public string AddressContract { get; set; }
        public string Currency { get; set; }
        public decimal AmountNow { get; set; }
        public decimal AmountNeed { get; set; }
        public decimal AmountTRXNeed { get; set; }
        public int UserId { get; set; }
        public int FileId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }
        public virtual ICollection<FavoriteProjectEntity> FavoriteProjects { get; set; } = new HashSet<FavoriteProjectEntity>();
        public virtual ICollection<ProcessEntity> Processes { get; set; }
        [ForeignKey("FileId")]
        public virtual FileEntity File { get; set; }
        public virtual ICollection<ArticalEntity> Articals { get; set; }
        public virtual ICollection<ProjectCategoryEntity> ProjectCategories { get; set; } = new HashSet<ProjectCategoryEntity>();

        public virtual ICollection<TransactionHistoryEntity> TransactionHistories { get; set; }
    }
}

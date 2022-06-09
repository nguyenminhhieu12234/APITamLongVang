using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Models.General;
using TuThienApi.Models.Project;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Models.Users
{
    public class UserEntity:IdentityUser<int>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public UserStatus Status { get; set; } = UserStatus.UnLock;
        public UserType Type { get; set; } = UserType.Organization;
        public DateTime CreateDate { get; set; }
        public int UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<ProjectEntity> Projects { get; set; }
        public virtual ICollection<FavoriteProjectEntity> FavoriteProjects { get; set; }
        public virtual ICollection<TransactionHistoryEntity> TransactionHistories { get; set; }
    }
}

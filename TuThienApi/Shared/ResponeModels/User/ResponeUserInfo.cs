using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Users;
using TuThienApi.Shared.ResponeModels.FollowProject;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.ResponeModels.User
{
    public class ResponeUserInfo
    {
        private readonly ApplicationDbContext _DbContext;

        public ResponeUserInfo(UserEntity user, ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            FullName = user.FullName;
            Type = user.Type;
            Email = user.Email;
            Address = user.Address;
            PhoneNumber = user.PhoneNumber;
            Avatar = user.Avatar;
            ProjectFollow = _DbContext.FavoriteProjectEntities.Where(x => x.UserId == user.Id && x.IsDeleted == false).Select(x => new FollowProjectRespone(x, _DbContext)).ToList();
        }

        public string FullName { get; set; }
        public UserType Type { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public List<FollowProjectRespone> ProjectFollow { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Users;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Shared.ResponeModels.TransactionHistory
{
    public class TransactionRespone
    {
        private readonly ApplicationDbContext _DbContext;
        public TransactionRespone(TransactionHistoryEntity transaction, ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
            var currentUser = _DbContext.Users.Where(x => x.Id == transaction.UserId).SingleOrDefault();
            UserName = currentUser != null ? currentUser.FullName : "Ẩn danh";
            UserAvatar = currentUser != null ? currentUser.Avatar : "\\uploads\\Images\\Anonymous_Avatar\\23052022_022411_anymous_icon.png";
            ProjectId = transaction.ProjectId;
            ProjectName = transaction.Project.Title;
            Amount = transaction.Amount;
            Currency = transaction.Currency;
            Hash = transaction.Hash;
            Type = transaction.Type;
            CreateTime = transaction.CreateTime;
        }

        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Hash { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

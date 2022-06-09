using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Users;
using TuThienApi.Shared.Global;
using TuThienApi.Shared.ResponeModels;
using TuThienApi.Shared.ResponeModels.Dashboard;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseApiController
    {
        public DashboardController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }


        [HttpGet("dash-board-statistical")]
        public async Task<AppRespone<object>> StatisticalDashBoard(int Year = 0)
        {
            try
            {
                var amountDonated = _DbContext.TransactionHistoryEntity.Where(x => x.Type == TransactionType.Donate).Sum(x => x.Amount);
                var projectCreated = _DbContext.Projects.Count();
                var donated = _DbContext.TransactionHistoryEntity.Where(x => x.Type == TransactionType.Donate).Count();
                var projectCompleted = _DbContext.Projects.Where(x => x.Status == ProjectStatus.Complete).Count();
                var projectImplementation = _DbContext.Projects.Where(x => x.Status == ProjectStatus.Implementation).Count();
                var projectWaitting = _DbContext.Projects.Where(x => x.Status == ProjectStatus.Wait).Count();
                var lockUsers = _DbContext.Users.Where(x => x.Status == UserStatus.Lock).Count();
                Year = DateTime.Now.Year;
                var donatedInMonth = _DbContext.TransactionHistoryEntity
                    .Where(delegate (TransactionHistoryEntity x) {
                        return (x.Type == TransactionType.Donate)
                        && (x.CreateTime.Year == Year || Year == DateTime.Now.Year);
                    })
                    .GroupBy(x => new { month = x.CreateTime.Month })
                    .Select(x => new { donated = x.Count(), x.Key.month })
                    .ToList();

                /*x => x.CreateTime.Year == Year || Year == DateTime.Now.Year*/

                var result = new StatisticalRespone(amountDonated, projectCreated, donated, 
                    projectCompleted, projectImplementation, projectWaitting, 
                    lockUsers, donatedInMonth);

                UpdateStatusProject updateStatus = new UpdateStatusProject(_DbContext);

                return AppRespone<object>.Success(result);
            }
            catch(Exception ex)
            {
                return AppRespone<object>.Failed("Lỗi, không thể thống kê dữ liệu!");
            }
        }

        [HttpGet("dash-board-ratings")]
        public async Task<AppRespone<object>> RatingsDashBoard()
        {
            try
            {
                var resultProject = _DbContext.Users
                    .Include(x => x.Projects)
                    .Select(x => new {
                        x.FullName,
                        sumProjectCreated = x.Projects.Count
                    })
                    .OrderByDescending(x => x.sumProjectCreated)
                    .Take(10)
                    .ToList();

                var resultTransaction = _DbContext.Users
                    .Include(x => x.TransactionHistories)
                    .Select(x => new
                    {
                        x.FullName,
                        sumAmountDonated = x.TransactionHistories.Where(x => x.Type == TransactionType.Donate).Sum(x => x.Amount)
                    })
                    .OrderByDescending(x => x.sumAmountDonated)
                    .Take(10)
                    .ToList();

                var result = new RatingsRespone(resultProject, resultTransaction);

                return AppRespone<object>.Success(result);
            }
            catch(Exception ex)
            {
                return AppRespone<object>.Failed("Lỗi, không thể thống kê được dữ liệu!");
            }
        }
    }
}

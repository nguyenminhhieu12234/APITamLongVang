using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Shared.ResponeModels;
using TuThienApi.Shared.ResponeModels.TransactionHistory;
using static TuThienApi.Shared.AppEnum;

namespace TuThienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : BaseApiController
    {
        public TransactionHistoryController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet("get-transaction")]
        public async Task<AppRespone<object>> GetTransaction(int currentindex = 0, TransactionType type = 0)
        {
            var result = _DbContext.TransactionHistoryEntity
                .Include(x => x.Project)
                .Where(x => x.Type == type || type == 0)
                .OrderByDescending(x => x.CreateTime)
                .Skip(currentindex * 12)
                .Take(12)
                .Select(x => new TransactionRespone(x, _DbContext))
                .ToList();

            return AppRespone<object>.Success(result);
        }
    }
}

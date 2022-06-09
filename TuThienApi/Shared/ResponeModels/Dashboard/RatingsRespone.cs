using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuThienApi.Data;
using TuThienApi.Models.Users;

namespace TuThienApi.Shared.ResponeModels.Dashboard
{
    public class RatingsRespone
    {
        public RatingsRespone(IEnumerable<object> ratingprojects, IEnumerable<object> ratingdonated)
        {
            RatingProjects = ratingprojects;
            RatingDonated = ratingdonated;
        }

        public IEnumerable<object> RatingProjects { get; set; }
        public IEnumerable<object> RatingDonated { get; set; }
    }
}

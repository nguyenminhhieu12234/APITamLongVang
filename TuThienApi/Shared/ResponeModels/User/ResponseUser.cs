using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuThienApi.Shared.ResponeModels.User
{
    public class ResponseUser
    {
        public ResponseUser()
        {

        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}

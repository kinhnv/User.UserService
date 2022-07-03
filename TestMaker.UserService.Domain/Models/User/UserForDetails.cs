using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.UserService.Domain.Models.User
{
    public class UserForDetails
    {
        public UserForDetails()
        {
            UserName = String.Empty;
            Password = String.Empty;
            RoleIds = new List<Guid>();
        }
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<Guid> RoleIds { get; set; }
    }
}

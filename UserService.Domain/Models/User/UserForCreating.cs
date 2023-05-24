using i3rothers.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Models.User
{
    public class UserForCreating
    {
        [ValidationStringProperty(IsRequired = true, MaxLength = 64)]
        public string UserName { get; set; } = null!;

        [ValidationStringProperty(IsRequired = true, MaxLength = 64)]
        public string Password { get; set; } = null!;

        [ValidationListProperty(IsRequired = true)]
        public List<Guid> RoleIds { get; set; } = null!;
    }
}

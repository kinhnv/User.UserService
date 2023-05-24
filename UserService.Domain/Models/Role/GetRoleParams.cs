using i3rothers.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Models.Role
{
    public class GetRoleParams
    {
        [ValidationGuidProperty(IsRequired = true)]
        public Guid RoleId { get; set; }
    }
}

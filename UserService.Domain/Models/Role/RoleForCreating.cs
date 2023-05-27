using i3rothers.Domain.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Models.Role
{
    public class RoleForCreating
    {
        [ValidationStringProperty(IsRequired = true, MaxLength = 64)]
        public string Name { get; set; } = null!;
    }
}

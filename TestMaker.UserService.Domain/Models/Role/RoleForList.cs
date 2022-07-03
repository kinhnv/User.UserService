using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.UserService.Domain.Models.Role
{
    public class RoleForList
    {
        public RoleForList()
        {
            Name = String.Empty;
        }
        public Guid RoleId { get; set; }

        public string Name { get; set; }
    }
}

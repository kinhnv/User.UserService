using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;

namespace TestMaker.UserService.Infrastructure.Entities
{
    public class RoleUser : Entity
    {
        public RoleUser()
        {
            IsDeleted = false;
        }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}

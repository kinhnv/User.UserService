using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;

namespace TestMaker.UserService.Infrastructure.Entities
{
    public class User : Entity
    {
        public User()
        {
            UserName = string.Empty;
            Password = string.Empty;
            IsDeleted = false;
        }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

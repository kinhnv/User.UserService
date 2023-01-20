using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;

namespace UserService.Infrastructure.Entities
{
    public class User : ObjectEntity
    {
        [Required]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(64)]
        public string UserName { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;
    }
}

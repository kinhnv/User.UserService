using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;

namespace UserService.Infrastructure.Entities
{
    public class Role : ObjectEntity
    {
        public Role()
        {
            Name = string.Empty;
            IsDeleted = false;
        }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }
    }
}

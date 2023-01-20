﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Repository;

namespace UserService.Infrastructure.Entities
{
    public class UserRole : ObjectEntity
    {
        public UserRole()
        {
            IsDeleted = false;
        }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}

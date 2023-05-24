﻿using i3rothers.Domain.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Models.User
{
    public class GetUserParams
    {
        [ValidationGuidProperty(IsRequired = true)]
        public Guid UserId { get; set; }
    }
}

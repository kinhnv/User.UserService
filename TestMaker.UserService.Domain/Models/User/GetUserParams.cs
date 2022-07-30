using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;

namespace TestMaker.UserService.Domain.Models.User
{
    public class GetUserParams : GetPaginationParams
    {
        public GetUserParams()
        {
            IsDeleted = false;
        }
    }
}

using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestMaker.UserService.Infrastructure.Entities;

namespace TestMaker.UserService.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static Claim[] ToClaims(this User user)
        {
            return new Claim[]
            {
                new Claim("user_id", user.UserId.ToString() ?? ""),
                //new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.Firstname) && !string.IsNullOrEmpty(user.Lastname)) ? (user.Firstname + " " + user.Lastname) : ""),
                //new Claim(JwtClaimTypes.GivenName, user.Firstname  ?? ""),
                //new Claim(JwtClaimTypes.FamilyName, user.Lastname  ?? ""),
                //new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

                //roles
                new Claim(JwtClaimTypes.Role, "Administrator")
            };
        }
    }
}

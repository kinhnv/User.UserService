using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Extensions;
using TestMaker.UserService.Infrastructure.Repositories.Users;

namespace TestMaker.UserService.Infrastructure.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IUsersRepository _usersRepository;

        public ProfileService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context?.Subject?.Identity?.Name))
                {
                    var user = (await _usersRepository.GetAsync(x => x.UserName == context.Subject.Identity.Name)).Single();

                    if (user != null)
                    {
                        var claims = user.ToClaims();

                        //set issued claims to return
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
                else if (context != null)
                {
                    //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                    //where and subject was set to my user id.
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                    {
                        //get user from db (find user by user id)
                        var user = (await _usersRepository.GetAsync(x => x.UserId == Guid.Parse(userId.Value))).Single();

                        // issue the claims for the user
                        if (user != null)
                        {
                            var claims = user.ToClaims();

                            context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                {
                    var user = (await _usersRepository.GetAsync(x => x.UserId == Guid.Parse(userId.Value))).Single();

                    if (user != null)
                    {
                        if (!user.IsDeleted)
                        {
                            context.IsActive = !user.IsDeleted;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
    }
}

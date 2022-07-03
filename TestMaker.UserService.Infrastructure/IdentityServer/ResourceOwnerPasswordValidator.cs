using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;
using TestMaker.UserService.Infrastructure.Entities;
using TestMaker.UserService.Infrastructure.Extensions;
using TestMaker.UserService.Infrastructure.Repositories.Users;

namespace TestMaker.UserService.Infrastructure.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsersRepository _usersRepository;
        public ResourceOwnerPasswordValidator(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var user = (await _usersRepository.GetAsync(x => x.UserName == context.UserName)).Single();
                if (user != null)
                {
                    //check if password match - remember to hash password if stored as hash in db
                    if (user.Password == context.Password)
                    {
                        //set the result
                        context.Result = new GrantValidationResult(
                            subject: user.UserId.ToString(),
                            authenticationMethod: "custom",
                            claims: user.ToClaims()
                        );

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
    }
}

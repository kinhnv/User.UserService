using IdentityServer4.Models;

namespace TestMaker.UserService.Infrastructure.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("testservice", "TestMaker.TestService"),
                new ApiScope("eventservice", "TestMaker.EventService"),
                new ApiScope("userservice", "TestMaker.UserService"),
            };
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "testplayer",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("testplayer".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "testservice", "eventservice" }
            }
        };
    }
}

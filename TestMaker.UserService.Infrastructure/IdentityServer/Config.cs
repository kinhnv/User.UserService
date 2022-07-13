using IdentityServer4.Models;

namespace TestMaker.UserService.Infrastructure.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("test-service", "TestMaker.TestService"),
                new ApiScope("event-service", "TestMaker.EventService"),
                new ApiScope("user-service", "TestMaker.UserService"),
            };
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "test-player",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("test-player".Sha256())
                },
                AllowedScopes = { "test-service", "event-service" },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            },
            new Client
            {
                ClientId = "administrator",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("administrator".Sha256())
                },
                AllowedScopes = { "test-service", "event-service", "user-service" },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            }
        };
    }
}

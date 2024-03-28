using System.Security.Claims;
using Identity.API.Constants;
using IdentityModel;
using IdentityServer4.Models;

namespace Identity.API.IdentityServerConfiguration;

public static class Configuration
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(ClaimTypes.Role, new[] { "role" })
        };

    public static IEnumerable<ApiResource> GetApis() =>
        new List<ApiResource>
        {
            new(IdentityServer4.IdentityServerConstants.LocalApi.ScopeName),
            new(IdentityServerConstants.ReviewApiResourceName, IdentityServerConstants.ReviewApiResourceDisplayName),
            new(IdentityServerConstants.BookApiResourceName, IdentityServerConstants.BookApiResourceDisplayName)
        };

    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = IdentityServerConstants.ClientId,
                ClientSecrets = { new Secret(IdentityServerConstants.ClientSecret.ToSha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,
                AccessTokenLifetime = 60 * 30, // 30 minutes
                IdentityTokenLifetime = 60 * 30, // 30 minutes
                AbsoluteRefreshTokenLifetime = 60 * 60 * 24, // 1 day
                UpdateAccessTokenClaimsOnRefresh = true,
                AllowedScopes =
                {
                    IdentityServer4.IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                    IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.ReviewApiResourceName,
                    IdentityServerConstants.BookApiResourceName,
                    IdentityServerConstants.RoleIdentityResourceName
                }
            }
        };

    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new(IdentityServerConstants.ReviewApiResourceName),
            new(IdentityServerConstants.BookApiResourceName),
            new(IdentityServer4.IdentityServerConstants.LocalApi.ScopeName)
        };
}
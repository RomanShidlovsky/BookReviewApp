using System.Security.Claims;
using Identity.API.Constants;
using Identity.DataAccess.Entities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.IdentityServerConfiguration;

public class ProfileService(UserManager<User> userManager) : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        context.IssuedClaims.AddRange(context.Subject.Claims);

        var user = await userManager.GetUserAsync(context.Subject);

        var roles = await userManager.GetRolesAsync(user);

        var rolesClaims = roles.Select(roleName =>
            new Claim(ClaimTypes.Role, roleName));

        context.IssuedClaims.AddRange(rolesClaims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);

        context.IsActive = user is { DateDeleted: null };
    }
}
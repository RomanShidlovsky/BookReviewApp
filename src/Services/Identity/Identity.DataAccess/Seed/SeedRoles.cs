using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Shared.Constants;

namespace Identity.DataAccess.Seed;

public static class SeedRoles
{
    public static async Task Add(RoleManager<Role> roleManager)
    {
        var roles = new[] { Roles.Client, Roles.Admin, Roles.Reviewer };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Role { Name = role } );
            }
        }
    }
}
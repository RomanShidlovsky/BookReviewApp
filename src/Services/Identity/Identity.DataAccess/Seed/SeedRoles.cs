using Identity.DataAccess.Entities;
using Identity.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;

namespace Identity.DataAccess.Seed;

public static class SeedRoles
{
    public static async Task Add(RoleManager<Role> roleManager)
    {
        var roles = Enum.GetNames(typeof(Roles));

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new Role { Name = role } );
            }
        }
    }
}
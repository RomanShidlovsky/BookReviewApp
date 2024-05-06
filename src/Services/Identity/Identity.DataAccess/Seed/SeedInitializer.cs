using Identity.DataAccess.Contexts;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DataAccess.Seed;

public class SeedInitializer(IServiceProvider serviceProvider)
{
    public async Task Init()
    {
        await SeedRoles.Add(serviceProvider.GetRequiredService<RoleManager<Role>>());
        await SeedUsers.Add(serviceProvider.GetRequiredService<UserManager<User>>());
    }
}
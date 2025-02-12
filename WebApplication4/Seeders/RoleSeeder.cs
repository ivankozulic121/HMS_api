
using Microsoft.AspNetCore.Identity;

namespace WebApplication4.Seeders;
public class RoleSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRoles()
    {
        string[] roleNames = { "Admin", "Doctor", "Patient" };

        foreach (var roleName in roleNames)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {   Console.WriteLine("EXECUTED!");
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        
        foreach (var role in _roleManager.Roles)
        {
            Console.WriteLine($"Role Name: {role.Name}");
        }
    }
}
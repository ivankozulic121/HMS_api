using Microsoft.AspNetCore.Identity;
using WebApplication4.Data;

namespace WebApplication4.Seeders;


public class AdminSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminSeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task SeedAdmin()
    {
        //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        string adminEmail = "zeljkoprovala@gmail.com";
        string adminPassword = "Zexon4231!";
        string adminRole = "Admin";
        
        Console.WriteLine("Admin Seed Executed.");
        
        if (!await _roleManager.RoleExistsAsync(adminRole))
        {
            await _roleManager.CreateAsync(new IdentityRole(adminRole));
        }
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);
        Console.WriteLine($"Admin User Exists: {adminUser}");

        if (adminUser == null)
        {
            Console.WriteLine($"Admin User Not Found: {adminEmail}");
            adminUser = new ApplicationUser
            {
                Email = adminEmail,
                UserName = adminEmail.Remove(adminEmail.IndexOf('@')),
                firstName = "Zeljko",
                lastName = "Markovic",

            };
            
            var createUser = await _userManager.CreateAsync(adminUser, adminPassword);
            Console.WriteLine($"Creating user succeded:{createUser}");
            
            if ( createUser.Succeeded) await _userManager.AddToRoleAsync(adminUser, adminRole); //dodijeli useru admin role
            
        }
    }
}
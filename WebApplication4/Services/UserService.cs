using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication4.Data;
using WebApplication4.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication4.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace WebApplication4.Services;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }
    
    //[HttpPost("addNewUser")]
    //[Authorize]
    public async Task AddNewUser(AddUserDto addUserDto)
    {
        var newUser = await _userManager.FindByEmailAsync(addUserDto.Email);

        if ( newUser == null )
        {
            
                newUser = new ApplicationUser
                {
                    Email = addUserDto.Email,
                    UserName = addUserDto.Email.Remove(addUserDto.Email.IndexOf('@')),
                    firstName = addUserDto.FirstName,
                    lastName = addUserDto.LastName,
                    userRole = addUserDto.userRole,
                    PasswordHash = addUserDto.Password

                };
                
                
                
            

            //kad se kreira user kreira se i pacijent ili doktor isto
            var createNewUser = await _userManager.CreateAsync(newUser, addUserDto.Password);
            Console.WriteLine($"Adding user succeded:{createNewUser}");

            if (createNewUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, addUserDto.userRole);

                if (addUserDto.userRole == "Patient")
                {
                    var newPatient = new Patient { User = newUser };
                    await _context.Patients.AddAsync(newPatient);
                    await _context.SaveChangesAsync();
                }

                else //ako nije patient mora biti doktor
                {
                    Console.WriteLine("DOCTOR!");
                    var newDoctor =  new Doctor { User = newUser };
                    await _context.Doctors.AddAsync(newDoctor);
                    await _context.SaveChangesAsync();
                }
            }
            //odje mozda
        }

        //await GenerateJwtToken(newUser);
    }
    
    public async Task<string> LoginUser(LoginUserDto loginUserDto)
    {
        var user =  await _userManager.FindByEmailAsync(loginUserDto.Email);
        //return (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.password));

        //if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.password))
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.password))
        {
            throw new Exception("Incorrect email or password");
        }
        //var token = GenerateJwtToken
        Console.WriteLine("USER:" + user);
        return await GenerateJwtToken(user);
    }

    public async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:5153"),
            new Claim(JwtRegisteredClaimNames.Iss, "http://localhost:5153"),
            new Claim(ClaimTypes.Name, user.firstName),
            new Claim(ClaimTypes.Name, user.lastName),
            
            
            //new Claim(ClaimTypes.Role, "Admin" )
        };
        
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var secretKey = _configuration["JwtSettings:SecretKey"];
        if (secretKey == null)
        {
            Console.WriteLine("NULL!");
        }
            Console.WriteLine("JWT Secret Key:" + secretKey);
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

        var token = new JwtSecurityToken(expires: DateTime.UtcNow.AddHours(2),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
        foreach (var claim in claims)
        {
            Console.WriteLine("CLAIMS:" + claim);
        }
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
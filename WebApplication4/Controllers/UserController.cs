using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTOs;
using WebApplication4.Services;

namespace WebApplication4.Controllers;

[ApiController]
[Route("/api/[controller]")]
//[Authorize]

public class UserController: ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddNewUser([FromBody] AddUserDto addUserDto)
    {
        
        await _userService.AddNewUser(addUserDto);
        return CreatedAtAction("AddNewUser",addUserDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        Console.WriteLine("EXECUTE LOGIN");
        var tokenString = await _userService.LoginUser(loginUserDto);
        //if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.password))
        return Ok(new { token = tokenString });
    }
    
        
    
}
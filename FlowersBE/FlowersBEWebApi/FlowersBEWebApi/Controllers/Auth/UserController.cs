namespace FlowersBEWebApi.Controllers.Auth;

using Microsoft.AspNetCore.Mvc;
using FlowersBEWebApi.Controllers;
using FlowersBEWebApi.Helpers;
using FlowersBEWebApi.Models;
using FlowersBEWebApi.Services.Auth;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

}
using FlowersBEWebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FlowersBEWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private static List<UserBase> _users = new();
        [HttpGet("getUser")]
        public async Task<ActionResult<UserBase>> Get()
        {
            var user = new UserBase
            {
                Id = 1,
                FirstName = "Rokas",
                LastName = "Bagdonas",
                Email = "x@gmail.com",
            };
            _users.Add(user);
            return Ok(user);
        }

        [HttpPost("newUser")]
        public async Task<ActionResult<List<UserBase>>> Create(UserBase user)
        {
            if (user == null)
                return BadRequest("User is null");
            _users.Add(user);
            return Ok(_users);
        }
    }
}

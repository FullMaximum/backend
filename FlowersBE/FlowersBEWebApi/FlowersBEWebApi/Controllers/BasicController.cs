using FlowersBEWebApi.Entities;
using FlowersBEWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowersBEWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly IBasicService _basicService;
        private readonly ILogger<BasicController> _logger;

        public BasicController(IBasicService basicService, ILogger<BasicController> logger)
        {
            _basicService = basicService;
            _logger = logger;
        }
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

        [HttpGet("getAll")]
        public async Task<ActionResult<List<UserBase>>> GetAll()
        {
            _logger.LogInformation($"We got request to {nameof(BasicController)}");
            return Ok(_basicService.GetUsers());
        }

        [HttpPost("newUser")]
        public async Task<ActionResult<List<UserBase>>> Create(UserBase user)
        {
            if (user == null)
                return BadRequest("User is null");
            _basicService.Add(user);
            return Ok("Created");
        }
    }
}

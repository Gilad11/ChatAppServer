using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            // Example: Return a list of users
            var users = new[]
            {
                new { Id = 1, Name = "Alice" },
                new { Id = 2, Name = "Bob" }
            };

            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            // Example: Return a single user
            var user = new { Id = id, Name = $"User{id}" };

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult CreateUser([FromBody] string userName)
        {
            // Example: Simulate user creation
            var createdUser = new { Id = 3, Name = userName };

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
    }
}
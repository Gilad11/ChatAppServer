using ChatAppServer.Data;
using ChatAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ChatAppDbContext context { get; set; }
        public UsersController(ChatAppDbContext _context)
        {
            context = _context;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = context.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var user = context.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("{id}/{user}")]
        public IActionResult EditUser(string id, [FromBody] User user) //mybe need to fix
        {
            context.EditUser(id, user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            context.DeleteUser(id);
            return NoContent();
        }

    }
}
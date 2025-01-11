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
        private ChatDbContext context { get; set; }
        public UsersController(ChatDbContext _context)
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

        // GET: api/users/{username}
        [HttpGet("{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            var user = context.GetUserByUsername(username);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            context.AddUser(user);
            return NoContent();
        }

        [HttpPost]
        public IActionResult EditUser(string username, User user)
        {
            context.EditUser(username, user);

            return NoContent();
        }

        [HttpPost]
        public IActionResult DeleteUser(string username)
        {
            context.DeleteUser(username);
            return NoContent();
        }

    }
}
using ChatAppServer.Data;
using ChatAppServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ChatDbContext context { get; set; }
        public ChatController(ChatDbContext _context)
        {
            context = _context;
        }

        // GET: api/chat/{mainUser}/{groupId}
        [HttpGet("{mainUserId}/{groupId}")]
        public IActionResult GetChatMessages(string mainUser, string groupId)
        {
            var conversation = context.GetChatMessagesById(mainUser, groupId);
            if (conversation == null)
            {
                return NoContent();
            }
            return Ok(conversation);
        }

        // POST: api/chat/{mainUserId}/{groupId}/{content}
        [HttpPost("{message}")]
        public IActionResult AddMessage(Message message)
        {
            context.AddMessage(message);
            return NoContent();
        }
        




    }
}

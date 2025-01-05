using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        // GET: api/chat
        [HttpGet]
        public IActionResult GetMessages()
        {
            return Ok(new { Message = "Hello from ChatController" });
        }

        // POST: api/chat
        [HttpPost]
        public IActionResult SendMessage([FromBody] string message)
        {
            // Process the message
            return Ok(new { Success = true, SentMessage = message });
        }
    }
}

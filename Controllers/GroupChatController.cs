using ChatAppServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {

        private ChatDbContext context { get; set; }
        public GroupChatController(ChatDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAllGroupsChat()
        {
            var groupsChat = context.GetAllGroupsChat();
            return Ok(groupsChat);
        }

        [HttpGet("{groupId}")]
        public IActionResult GetGroupChatById(string groupId)
        {
            var groupChat = context.GetGroupChatById(groupId);
            return Ok(groupChat);
        }

        [HttpPost("{groupName}")]
        public IActionResult AddGroupChat(string groupName)
        {
            context.AddGroupChat(groupName);
            return NoContent();
        }

        [HttpPost("{groupId}")]
        public IActionResult DeleteGroupChat(string groupId)
        {
            context.DeleteGroupChat(groupId);
            return NoContent();
        }

        [HttpPost("{groupId}/{groupName}")]
        public IActionResult EditGroupChatName(string groupId, string groupName)
        {
            context.EditGroupChatName(groupId, groupName);
            return NoContent();
        }

    }
}

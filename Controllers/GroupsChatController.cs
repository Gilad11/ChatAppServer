using ChatAppServer.Data;
using ChatAppServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsChatController : ControllerBase
    {

        private ChatDbContext context { get; set; }
        public GroupsChatController(ChatDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetAllGroupsChat()
        {
            var groupsChat = context.GetAllGroupsChat();
            if (groupsChat == null)
            {
                return Ok("No groups chat for this user!");
            }
            return Ok(groupsChat);
        }

        [HttpGet("{groupId}")]
        public IActionResult GetGroupChatById(string groupId)
        {
            var groupChat = context.GetGroupChatById(groupId);
            return Ok(groupChat);
        }

        [HttpPost("{groupChat}")]
        public IActionResult AddGroupChat(GroupChat groupChat)
        {
            context.AddGroupChat(groupChat);
            return NoContent();
        }

        [HttpDelete("{groupId}")]
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

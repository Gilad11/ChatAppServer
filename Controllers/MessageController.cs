using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ChatAppServer.Models;
using System.Threading.Tasks;
using System;
using ChatAppServer.Data;
using System.Reflection;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly ChatAppDbContext _context;
    private readonly ChatHub _chatHub; 

    public MessageController(ChatAppDbContext context, ChatHub chatHub)
    {
        _context = context;
        _chatHub = chatHub;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] Message message) //need to save on DB
    {
        _context.Messages.Add(message);
        _context.SaveChanges();
        await _chatHub.SendMessage(message.SenderId, message.ReceiverId, message.Content);

        return Ok(message);
    }

    [HttpGet("{user1Id}/{user2Id}")]
    public IActionResult GetMessagesBetweenUsers(string user1Id, string user2Id)
    {
        var messages = _context.GetMessagesBetweenUsers(user1Id, user2Id);
        return Ok(messages);
    }


}

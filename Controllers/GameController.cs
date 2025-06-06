﻿using ChatAppServer.Data;
using ChatAppServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ChatAppDbContext _context;
        private readonly ChatHub _chatHub;

        // Constructor to inject dependencies
        public GameController(ChatAppDbContext context, ChatHub chatHub)
        {
            _context = context;
            _chatHub = chatHub;
        }

        [HttpGet("getgame/{senderId}/{receiverId}")]
        public async Task<IActionResult> GetGame(string senderId, string receiverId)
        {
            var game = _context.GetGame(senderId, receiverId);
            return Ok(game);
        }

        [HttpGet("getgames/{userId}")]
        public async Task<IActionResult> GetGames(string userId)
        {
            var game = _context.GetGames(userId);
            return Ok(game);
        }

        [HttpPost("savegame")]
        public async Task<IActionResult> SaveGame([FromBody] Game newGame)
        {
            _context.SaveGame(newGame);
            await _chatHub.SendGame(newGame);
            return Ok();
        }
    }
}
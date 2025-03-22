using ChatAppServer.Data;
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

        /*  [HttpPost("reset/{gameId}")]
          public async Task<IActionResult> ResetGame(string gameId)
          {
              var movesToDelete = _context.Moves.Where(m => m.GameId == gameId);
              _context.Moves.RemoveRange(movesToDelete);
              await _context.SaveChangesAsync();
              return Ok($"Game {gameId} reset successfully.");
          }*/




        /* [HttpGet("moves/{gameId}")]
         public async Task<IActionResult> GetMoves(string gameId)
         {
             var moves = await _context.Moves
                 .Where(m => m.GameId == gameId)
                 .OrderBy(m => m.X)
                 .ThenBy(m => m.Y)
                 .ToListAsync();

             return Ok(moves);
         }*/


        [HttpGet("getgame/{u1}/{u2}")]
        public async Task<IActionResult> GetGame(string u1, string u2)
        {
            string gameId = _context.FindGameId(u1, u2);
            if (gameId == "0") return BadRequest("non unique username");
            var game = _context.IsExistGame(gameId, u1, u2);
            return Ok(game);
        }
       
        /* [HttpPost("makemove/{u1}{u2}")]
         public async Task<IActionResult> MakeMove(string u1, string u2, [FromBody] Move move)
         {

             _context.MakeMove()
         }*/
    }
}
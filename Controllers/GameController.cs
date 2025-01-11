using ChatAppServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppServer.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private ChatDbContext context { get; set; }
        public GameController(ChatDbContext _context)
        {
            context = _context;
        }

        // GET: api/game
        [HttpGet]
        public IActionResult GetGameState()
        {
            // Example: Return the current game state
            var gameState = new
            {
                GameId = 1,
                Board = new int[8, 8], // Simplified: Empty board
                CurrentPlayer = "Player1"
            };

            return Ok(gameState);
        }

        // POST: api/game/start
        [HttpPost("start")]
        public IActionResult StartGame()
        {
            // Example: Simulate starting a new game
            var newGame = new
            {
                GameId = 1,
                Message = "New game started!"
            };

            return Ok(newGame);
        }

        // POST: api/game/move
        [HttpPost("move")]
        public IActionResult MakeMove([FromBody] object move)
        {
            // Example: Simulate making a move
            var moveResult = new
            {
                Success = true,
                Message = "Move accepted."
            };

            return Ok(moveResult);
        }
    }
}
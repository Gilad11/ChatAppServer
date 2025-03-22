using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppServer.Models
{
    public class Game
    {
        public Game(string id, string player1, string player2)
        {
            Id = id;
            Player1 = player1;
            Player2 = player2;
        }

        public string Id { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }

        // Foreign Key for Board
        [ForeignKey("GameBoard")]  // Explicitly mark this as the foreign key
        public int GameBoardId { get; set; }
        public Board GameBoard { get; set; }
    }
}

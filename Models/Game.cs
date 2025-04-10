using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    public class Game
    {
        //1 = mouse, 2 = cat, 3 = dog
        public int Id { get; set; }
        public int cell1 { get; set; } = 0; // -3 -> 3
        public int cell2 { get; set; } = 0;
        public int cell3 { get; set; } = 0;
        public int cell4 { get; set; } = 0;
        public int cell5 { get; set; } = 0;
        public int cell6 { get; set; } = 0;
        public int cell7 { get; set; } = 0;
        public int cell8 { get; set; } = 0;
        public int cell9 { get; set; } = 0;

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }
        [Required]
        public string RedId { get; set; }

    }
}

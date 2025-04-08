using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    /*
     * 1,2,3
     * 4,5,6
     * 7,8,9
     */
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public int cell1 { get; set; } = 0;
        public int cell2 { get; set; } = 0;
        public int cell3 { get; set; } = 0;
        public int cell4 { get; set; } = 0;
        public int cell5 { get; set; } = 0;
        public int cell6 { get; set; } = 0;
        public int cell7 { get; set; } = 0;
        public int cell8 { get; set; } = 0;
        public int cell9 { get; set; } = 0;
    }
}

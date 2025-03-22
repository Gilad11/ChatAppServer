using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models
{
    public class Move
    {
        [Required] //1-9
        public int cell { get; set; }

        [Required] //1 = mouse, 2 = cat, 3 = dog
        public int animal { get; set; } = 0;
    }
}

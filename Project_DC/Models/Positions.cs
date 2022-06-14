using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
    public class Positions
    {
        [Key]
        public int Id_position { get; set; }
        public string position { get; set; }
    }
}

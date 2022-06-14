using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
    public class Rooms
    {
        [Key]
        public int RoomsId { get; set; }
        public string RoomsName { get; set; }

    }
}

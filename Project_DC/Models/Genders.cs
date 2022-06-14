using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
    public class Genders
    {
        [Key]
        public int id_gender { get; set; }
        public string gender { get; set; }
    }
}

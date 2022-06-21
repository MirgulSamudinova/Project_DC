using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
    public class Units
    {
        public int UnitsId { get; set; }

        [Display(Name = "Единица измерения")]
        public string UnitsName { get; set; }

    }
}

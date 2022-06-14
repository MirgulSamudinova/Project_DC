using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
    public class DaysOfWeek
    {
        [Key]
        public int DaysOfWeekID { get; set; }
        public string DaysOfWeekName { get; set; }

    }
}

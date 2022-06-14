using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
    public class ServicesGroup
    {
        [Key]
        public int IdGroup { get; set; }
        public string GroupName { get; set; }
    }
}

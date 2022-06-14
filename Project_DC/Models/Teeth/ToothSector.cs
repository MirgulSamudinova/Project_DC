using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
    public class ToothSector
    {
        public int Id { get; set; }

        [Display(Name = "Наименование сектора")]
        public string Name { get; set; }

        [Display(Name = "Номер сектора")]
        public int NumberOfSector { get; set; }
    }
}

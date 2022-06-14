using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_DC.Models
{
    public class Materials
    {
        [Key]
        public int MaterialId { get; set; }
        public string Name { get; set; }
        public int count { get; set; }

        public int unit { get; set; }
        public double  price { get; set; }
        public double summ_price { get; set; }

        [ForeignKey("unit") ]
        public Units? Units { get; set; }
    }
}

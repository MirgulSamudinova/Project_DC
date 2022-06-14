using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_DC.Models
{
    public class Services
    {
        [Key]
        public int IdService { get; set; }
        public string ServiceName { get; set; }
        public decimal price { get; set; }
        public  string typeService { get; set; }

        
        public int idGroup { get; set; }

        [ForeignKey("idGroup")]
        public ServicesGroup? ServicesGroup { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_DC.Models
{
    public class Staffs
    {
        [Key]
        public int id_staff { get; set; }
        public string sure_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }

        public int id_gender { get; set; }
        public int id_position { get; set; }

         [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birth_date { get; set; }
        public int mobile_number { get; set; }
        public string e_mail { get; set; }

        [ForeignKey("id_position")]
        public Positions? Positions { get; set; }
       
        [ForeignKey("id_gender")]
        public Genders? Genders { get; set; }

        public string FullName
        {
            get
            {
                return sure_name + "  " + first_name + " "+ middle_name;
            }
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_DC.Models
{
    public class Patients
    {
        [Key]
        public int PatientId { get; set; }
        public string LastName { get; set; }

        public string  FirstName { get; set; }
        public string  MiddleName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
       
        public int  GenderId { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string e_mail { get; set; }

        public string inn { get; set; }


        [ForeignKey("GenderId")]
        public Genders? Genders { get; set; }    

       

    }
}

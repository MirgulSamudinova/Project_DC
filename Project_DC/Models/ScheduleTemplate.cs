using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_DC.Models
{
    public class ScheduleTemplate
    {
        [Key]
        public int  TmeplateId { get; set; }

        [Required]
        public int StaffId { get; set; }

        [Required]

        public int  DayOfWeek { get; set; }
         
        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan withTime { get; set; }
        [Required]
        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan ToTime { get; set; }

        [ForeignKey("StaffId")]
        public Staffs? Staffs { get; set; }

        [ForeignKey("DayOfWeek")]
        public DaysOfWeek? DaysOfWeek { get; set; }

    }
}


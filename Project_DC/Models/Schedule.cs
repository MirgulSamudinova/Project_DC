using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
   
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public int StaffId { get; set; }
        public DateTime withDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TemplateID { get; set; }

        public Staffs? Staffs { get; set; }

    }
}

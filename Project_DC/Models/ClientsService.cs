using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class ClientsService
	{
		public int Id { get; set; }

		[Display(Name = "ФИО пациента")]
		public int PatientsId { get; set; }
		public Patients? _Patients { get; set; }
		
		[Display(Name = "ФИО врача")]
		public int StaffsId { get; set; }
		public Staffs? _Staffs { get; set; }

		[Display(Name = "Дата и время прёма")]
		[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy, HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime ServiceDate { get; set; }

		[Display(Name = "Общие услуги")]
		public List<GeneralService>? GeneralServices { get; set; }

		[Display(Name = "Работа с зубами")]
		public List<ToothService>? ToothServices { get; set; }

		public ClientsService()
		{
		}
	}
}


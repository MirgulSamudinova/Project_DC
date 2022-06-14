using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class ToothService : GeneralService
	{
		[Display(Name = "Зуб")]
		public ClientsTooth? _ClientsTooth { get; set; }
		public int ClientsToothId { get; set; }

		public ToothService()
		{
		}
	}
}


using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class DentalService
	{
		public int Id { get; set; }

		[Display(Name = "Наименование услуги")]
		public string NameOfService { get; set; }

		[Display(Name = "Группа услуг")]
		public int DentalServiceGroupId { get; set; }
		public DentalServiceGroup? _DentalServiceGroup { get; set; }
		

		[Display(Name = "Определённый зуб")]
		public bool IsToothService { get; set; }

		[Display(Name = "Цена")]
		public double Price { get; set; }

		public DentalService()
		{
		}
	}
}


using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class GeneralService
	{
		public int Id { get; set; }

		[Display(Name = "Услуга")]
		public DentalService? _DentalService { get; set; }
		[Display(Name = "Услуга")]
		public int DentalServiceId { get; set; }

		[Display(Name = "Комментарии")]
		public string Comment { get; set; }

		[Display(Name = "Цена")]
		public double Price { get; set; }

		public GeneralService()
		{
		}
	}
}


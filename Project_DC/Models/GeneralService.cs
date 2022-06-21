using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class GeneralService
	{
		public int Id { get; set; }

		[Display(Name = "Услуга")]
		public int DentalServiceId { get; set; }
		public DentalService? _DentalService { get; set; }

		[Display(Name = "Услуга")]
		public int ClientsServiceId { get; set; }
		public ClientsService? _ClientsService { get; set; }
		

		[Display(Name = "Комментарии")]
		[Required]
		public string Comment { get; set; }

		[Display(Name = "Цена")]
		[Range(1, int.MaxValue, ErrorMessage = "Сумма должна быть больше {1}")]
		public double Price { get; set; }

		public GeneralService()
		{
		}
	}
}


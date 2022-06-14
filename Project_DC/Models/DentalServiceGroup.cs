using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class DentalServiceGroup
	{
		public int Id { get; set; }

		[Display(Name = "Наименование групп услуг")]
		public string DentalServiceGroupName { get; set; }

		public DentalServiceGroup()
		{
		}
	}
}


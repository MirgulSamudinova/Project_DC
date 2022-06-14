using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class ToothType
	{
		public int Id { get; set; }

        [Display(Name = "Тип зуба")]
		public string ToothTypeName { get; set; }

		public ToothType()
		{

		}
	}
}


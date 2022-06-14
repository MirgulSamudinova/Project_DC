using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class ToothState
	{
		public int Id { get; set; }

		[Display(Name = "Состояние зуба")]
		public string ToothStateName { get; set; }

		public string ToothStateColor { get; set; }

		public ToothState()
		{
		}
	}
}


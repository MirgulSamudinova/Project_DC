using System;
using System.ComponentModel.DataAnnotations;

namespace Project_DC.Models
{
	public class Client
	{
		public int Id { get; set; }

		[Display(Name = "ФИО")]
		public string FIO { get; set; }

		[Display(Name = "Дата рождения")]
		public DateTime DateOfBirth { get; set; }

		[Display(Name = "Состояние здоровья")]
		public string HealthData { get; set; }

		public virtual List<ClientsTooth>? ClientsTeeth { get; set; }


		internal string DateOfBirth_value {
            get {
				string date = "";

				date = DateOfBirth != null ? DateOfBirth.ToString("dd.MM.yyyy") : "";

				return date;
			}
		}

		public Client()
		{
		}
	}
}


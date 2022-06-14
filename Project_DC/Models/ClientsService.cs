using System;
namespace Project_DC.Models
{
	public class ClientsService
	{
		public int Id { get; set; }

		public Client? _Client { get; set; }
		public int ClientId { get; set; }

		public int StaffId { get; set; }

		public DateTime ServiceDate { get; set; }

		public List<GeneralService>? Services { get; set; }

		public List<ClientsTooth>? ClientsTeeth { get; set; }

		public string ServiceDateValue { get
            {
				return ServiceDate.ToString("dd.MM.yyyy");

			}
		}

		public ClientsService()
		{
		}
	}
}


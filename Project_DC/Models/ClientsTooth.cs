using System;
using System.Text.Json.Serialization;

namespace Project_DC.Models
{
	public class ClientsTooth
	{
		public int Id { get; set; }

		[JsonIgnore]
		public Patients? _Patients { get; set; }
		public int PatientsId { get; set; }

		public Tooth? _Tooth { get; set; }
		public int ToothId { get; set; }

		public ToothState? _ToothState { get; set; }
		public int ToothStateId { get; set; }

		[JsonIgnore]
		public List<ToothService>? ToothServices { get; set; }

		public string ToothDetails
        {
            get
            {
				string det = _Tooth != null ? _Tooth.ToothDetails : "";
				return det;
			}
        }

		public ClientsTooth()
		{
		}
	}
}


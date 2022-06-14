using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project_DC.Models
{
	public class Tooth : ICloneable
	{
		public int Id { get; set; }

		[Display(Name = "Наименование зуба")]
		public string Name { get; set; }

		[Display(Name = "Номер зуба")]
		public int CurrentNumber { get; set; }

		
		public int ToothSectorId { get; set; }

		[Display(Name = "Секция")]
		[JsonIgnore]

		public ToothSector? _ToothSector { get; set; }

		public int ToothTypeId { get; set; }

		[Display(Name = "Тип зуба")]
		public ToothType? _ToothType { get; set; }

		[Display(Name = "ID зуба")]
		public string? ToothId { 

			get {
				string name = "";
				name = (_ToothSector != null ? _ToothSector.NumberOfSector.ToString() : "" ) + (CurrentNumber != 0 ? CurrentNumber.ToString() : "");
				return name;
            } 
		}

		public string? ToothDetails
		{

			get
			{
				string name = "";
				name = (_ToothSector != null ? _ToothSector.NumberOfSector.ToString() : "") + (CurrentNumber != 0 ? CurrentNumber.ToString() : "");
				name = String.Format("{0} - {1}", name, Name);
				return name;
			}
		}

		public Tooth()
		{
			
		}

		public object Clone()
		{
			var newTooth = this.MemberwiseClone() as Tooth;
			/*newTooth.Name = this.Name;
			newTooth.CurrentNumber = this.CurrentNumber;
			newTooth.ToothSectorId = this.ToothSectorId;
			newTooth._ToothSector = this._ToothSector;
			newTooth.ToothTypeId = this.ToothTypeId;
			newTooth._ToothType = this._ToothType;*/
			return newTooth;
		}
	}
}


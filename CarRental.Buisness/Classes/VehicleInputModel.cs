using CarRental.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Buisness.Classes
{
	public class VehicleInputModel
	{
		public string? RegNumber { get; set; }
		public string? Make { get; set; }
		public double? Odometer { get; set; }
		public VehicleTypes VehicleType { get; set; }
		public double? CostKm { get; set; }
		public double? DailyCost {  get; set; }

		public double? Distance { get; set; }
		
	}

}

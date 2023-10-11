using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using System.Text.Json.Serialization;

namespace CarRental.Common.Classes
{
	public class Motorcycle : IVehicle
	{
		public string Make { get; init; }
		public string RegNumber { get; init; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public VehicleTypes VehicleType { get; init; }
		public VehicleStatuses VehicleStatus { get; set; } = VehicleStatuses.Available;

		public double DailyCost { get; init; }
		public double KmCost { get; init; }
		public double Odometer { get; set; }



		public Motorcycle(string make, string regnumber, VehicleTypes vehicletype, double dailycost, double kmcost, double odometer)
		{
			Make = make;
			RegNumber = regnumber;
			VehicleType = vehicletype;
			DailyCost = dailycost;
			KmCost = kmcost;
			Odometer = odometer;

		}

	}
}

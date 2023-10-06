using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using System.Text.Json.Serialization;

namespace CarRental.Common.Classes
{
	public class Car : IVehicle
	{
		public string Make { get; set; }
		public string RegNumber { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public VehicleTypes VehicleType { get; set; }


		public double DailyCost { get; set; }
		public double KmCost { get; set; }
		public double Odometer { get; set; }


		public Car(string make, string regnumber, VehicleTypes vehicletype, double dailycost, double kmcost, double odometer)
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

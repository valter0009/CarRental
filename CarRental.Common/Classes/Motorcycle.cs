using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using System.Text.Json.Serialization;

namespace CarRental.Common.Classes
{
	public class Motorcycle : Vehicle
	{
		


		public Motorcycle(string make, string regnumber, VehicleTypes vehicletype, double dailycost, double kmcost, double odometer):base(make, regnumber, vehicletype, dailycost, kmcost, odometer)
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

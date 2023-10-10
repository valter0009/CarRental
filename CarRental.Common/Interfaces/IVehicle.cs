using CarRental.Common.Enums;
using System.Text.Json.Serialization;

namespace CarRental.Common.Interfaces;

public interface IVehicle : IBase
{
	string Make { get; set; }
	string RegNumber { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	VehicleTypes VehicleType { get; set; }

	double DailyCost { get; set; }
	double KmCost { get; set; }
	double Odometer { get; set; }


}

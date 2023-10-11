using CarRental.Common.Enums;
using System.Text.Json.Serialization;

namespace CarRental.Common.Interfaces;

public interface IVehicle : IBase
{
	string Make { get; init; }
	string RegNumber { get; init; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	VehicleTypes VehicleType { get; init; }
	VehicleStatuses VehicleStatus { get; set; }
	double DailyCost { get; init; }
	double KmCost { get; init; }
	double Odometer { get; set; }


}

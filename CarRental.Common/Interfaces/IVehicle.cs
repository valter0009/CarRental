using CarRental.Common.Enums;
using System.Text.Json.Serialization;

namespace CarRental.Common.Interfaces;

public interface IVehicle: IBase
{
	public string Make { get; set; }
	public string RegNumber { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public VehicleTypes VehicleType { get; set; }

	public double DailyCost { get; set; }
	public double KmCost { get; set; }
	public double Odometer { get; set; }

	
}

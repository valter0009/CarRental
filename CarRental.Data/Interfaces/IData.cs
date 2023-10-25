using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CarRental.Common.Classes;
using CarRental.Common.Enums;
using CarRental.Common.Interfaces;

namespace CarRental.Data.Interfaces;

public interface IData
{
	int NextVehicleId { get; }
	int NextPersonId { get; }
	int NextBookingId { get; }
	async Task LoadDataFromJson() { }

	List<T> Get<T>(Expression<Func<T, bool>>? expression) where T : IBase;
	T? Single<T>(Expression<Func<T, bool>>? expression) where T : IBase;
	public void Add<T>(T item) where T : IBase;

	public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));
	public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
	public IBooking RentVehicle(int vehicleId, int customerId);
	public IBooking ReturnVehicle(int vehicleId, double kmReturned);
	public VehicleTypes GetVehicleType(string name)
	{
		if (Enum.TryParse(name, out VehicleTypes vehicleType))
		{
			return vehicleType;
		}
		else
		{
			throw new ArgumentException("Invalid vehicle type name");
		}
	}

	
}

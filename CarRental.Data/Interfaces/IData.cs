using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Common.Enums;
using CarRental.Common.Interfaces;

namespace CarRental.Data.Interfaces;

public interface IData
{
	IEnumerable<IPerson> GetPersons();
	IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default);
	IEnumerable<IBooking> GetBookings();
	public async Task LoadDataFromJson() { }

	/*
	 * IBooking GetBooking(int vehicleID);
	 * IPerson GetPerson( string socialSecurityNumber);
	 * IPerson GetPerson(int id);
	 * IVehicle GetVehicle(string registrationNumber);
	 * IVehicle GetVehicle(int id); */
}

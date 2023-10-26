using CarRental.Common.Classes;
using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using CarRental.Data.Interfaces;
using System.Xml.Linq;


namespace CarRental.Buisness.Classes;

public class BookingProcessor
{
	private readonly IData _db;


	public BookingProcessor(IData db) => _db = db;

	public async Task LoadData()
	{
		await _db.LoadDataFromJson();
	}

	public IEnumerable<IBooking> GetBookings()
	{
		return _db.Get<IBooking>(null);
	}
	public IEnumerable<IPerson> GetCustomers()
	{
		return _db.Get<IPerson>(null);

	}
	public IPerson? GetPerson(int ssn)
	{
		return _db.Single<IPerson>(x => x.SocialSecurityNumber.Equals(ssn));
	}
	public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default)
	{
		return _db.Get<Vehicle>(null);
	}
	public Vehicle? GetVehicle(int vehicleId)
	{
		return _db.Single<Vehicle>(x => x.Id == vehicleId);
	}
	public Vehicle? GetVehicle(string regNo)
	{
		return _db.Single<Vehicle>(x => x.RegNumber == regNo);
	}
	public async Task RentVehicle(int vehicleId, int
   customerId)
	{
		await Task.Delay(5);
		_db.RentVehicle(vehicleId, customerId);
	}
	public IBooking ReturnVehicle(int vehicleId, double distance)
	{
		return _db.ReturnVehicle(vehicleId, distance);
	}
	public void AddVehicle(string make, string regnumber, VehicleTypes vehicletype, double dailycost, double kmcost, double odometer)
	{
		if (vehicletype == VehicleTypes.Motorcycle)
		{
			Motorcycle vehicle = new Motorcycle(make, regnumber, vehicletype, dailycost, kmcost, odometer);
			_db.Add<Vehicle>(vehicle);
		}

		else
		{
			Car vehicle = new Car(make, regnumber, vehicletype, dailycost, kmcost, odometer);
			_db.Add<Vehicle>(vehicle);
		}


	}
	public void AddCustomer(string firstname, string lastname, int socialsecuritynumber)
	{
		Customer customer = new Customer(firstname, lastname, socialsecuritynumber);
		_db.Add<IPerson>(customer);
	}
	// Calling Default Interface Methods
	public string[] VehicleStatusNames => _db.VehicleStatusNames;
	public string[] VehicleTypeNames => _db.VehicleTypeNames;
	public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);

}
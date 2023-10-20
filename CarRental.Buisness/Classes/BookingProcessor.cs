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

	public IEnumerable<IPerson> GetCustomer()
	{
		
		return _db.GetPersons();


	}

	
	public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default) {
		
		return _db.GetVehicles();

	}

	public IEnumerable<IBooking> GetBookings() {

		
		return _db.GetBookings();
		
		
		
	}

	
 /* public void BookVehicle(IVehicle vehicle, Customer customer)
	{
		Booking booking = new(vehicle, customer);
		
		
	}
 */
	public void ReturnVehicle(IBooking booking, double kmreturned)

	{
		booking.KmReturned = kmreturned;
		booking.ReturnedDate = DateTime.Now;
		booking.BookingStatus = false;
		booking.TotalCost = CalculateDailyCost(booking, booking.RentedDate, booking.ReturnedDate, booking.Vehicle.DailyCost) + CalculateKmCost(booking, booking.KmReturned);
		booking.Vehicle.VehicleStatus = VehicleStatuses.Available;

	}

	double CalculateKmCost(IBooking booking, double kmreturned)
	{
		if (kmreturned < booking.Vehicle.Odometer) throw new Exception("Km count when returned cannot be less than the vehicle's odometer reading.");
		double KmCost = (kmreturned - booking.Vehicle.Odometer) * booking.Vehicle.KmCost;
		return KmCost;
	}
	public double CalculateDailyCost(IBooking booking, DateTime rented, DateTime returned, double dailycost)
	{
		TimeSpan timeDifference = rented - returned;
		int daysDifference = timeDifference.Days;
		if (daysDifference < 1) return dailycost;
		else return dailycost * daysDifference;
		
	}



	/*

 public IEnumerable<IBooking> GetBookings() { }
 public IEnumerable<Customer> GetCustomers() { }
 public IPerson? GetPerson(string ssn) { }
 public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default){ }
 public Vehicle? GetVehicle(int vehicleId) { }
 public Vehicle? GetVehicle(string regNo) { }
 public lägg till asynkron returdata typ RentVehicle(int vehicleId, int
customerId)
 {
 // Använd Task.Delay för att simulera tiden det tar
 // att hämta data från ett API.
 }
 public IBooking ReturnVehicle(int vehicleId, double ditance) { }
 public void AddVehicle(string make, string registrationNumber, double
odometer, double costKm, VehicleStatuses status, VehicleTypes type) { }
 public void AddCustomer(string socialSecurityNumber, string firstName, string
lastName) { }
 // Calling Default Interface Methods
 public string[] VehicleStatusNames => _db.VehicleStatusNames;
 public string[] VehicleTypeNames => _db.VehicleTypeNames;
 public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);
	*/
}
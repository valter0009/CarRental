using CarRental.Common.Classes;
using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using CarRental.Data.Interfaces;


namespace CarRental.Buisness.Classes;

public class BookingProcessor
{
	private readonly IData _db;
	public string ErrorMessage { get; set; } = string.Empty;
	public bool RentingInProgress { get; set; } = false;


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
	public IEnumerable<Vehicle> GetVehicles()
	{
		return _db.Get<Vehicle>(null);
	}
	public async Task RentVehicle(int vehicleId, int
   customerId)
	{
		try
		{

			if (GetBookings().Any(x => x.Vehicle.Id.Equals(vehicleId)
			&& x.Vehicle.VehicleStatus.Equals(VehicleStatuses.Booked)))
			{
				ErrorMessage = "Vehicle is already booked, please select another vehicle.";
			}
			else if (GetBookings().Any(x => x.Customer.Id.Equals(customerId) && x.Vehicle.VehicleStatus.Equals(VehicleStatuses.Booked)))
			{
				ErrorMessage = "This customer already has a booking in their name. Please select another customer.";
			}
			else
			{
				RentingInProgress = true;
				await Task.Delay(3000);
				_db.RentVehicle(vehicleId, customerId);

			}
		}
		catch (Exception ex) { ErrorMessage = ex.Message; }
		finally { RentingInProgress = false; }
	}
	public IBooking ReturnVehicle(int vehicleId, double? distance)
	{
		return _db.ReturnVehicle(vehicleId, distance);
	}
	public void AddVehicle(string make, string regnumber, VehicleTypes vehicletype, double? dailycost, double? kmcost, double? odometer)
	{
		if (string.IsNullOrWhiteSpace(make) || string.IsNullOrWhiteSpace(regnumber) || dailycost == null || kmcost == null || odometer == null)
		{
			ErrorMessage = "Please fill in all required fields.";
			return;
		}

		if (CheckIfVehicleExists(regnumber))
		{
			ErrorMessage = "Vehicle with the same registration number already exists.";
		}
		else
		{
			Vehicle vehicle;

			if (vehicletype == VehicleTypes.Motorcycle)
			{
				vehicle = new Motorcycle(make, regnumber, vehicletype, dailycost, kmcost, odometer);
			}
			else
			{
				vehicle = new Car(make, regnumber, vehicletype, dailycost, kmcost, odometer);
			}

			_db.Add<Vehicle>(vehicle);
		}
	}


	public void AddCustomer(string firstname, string lastname, int? socialsecuritynumber)
	{
		if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) || socialsecuritynumber == null)
		{
			ErrorMessage = "Please fill in all required fields.";
			return;
		}

		if (CheckIfCustomerExists(firstname, lastname, socialsecuritynumber))
		{
			ErrorMessage = "Customer with the same first name, last name, and SSN already exists.";
		}
		else
		{
			Customer customer = new(firstname, lastname, socialsecuritynumber);
			_db.Add<IPerson>(customer);
		}
	}


	private bool CheckIfVehicleExists(string regnumber)
	{
		return GetVehicles().Any(x => x.RegNumber == regnumber);
	}
	private bool CheckIfCustomerExists(string firstname, string lastname, int? socialsecuritynumber)
	{
		return GetCustomers().Any(x =>
			x.FirstName == firstname &&
			x.LastName == lastname &&
			x.SocialSecurityNumber == socialsecuritynumber);
	}
	
	public string[] VehicleStatusNames => _db.VehicleStatusNames;
	public string[] VehicleTypeNames => _db.VehicleTypeNames;
	public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);

}
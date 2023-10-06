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

	
	public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default) {

		return _db.GetVehicles();
	}

	//public IEnumerable<IBooking> GetBookings() { }
}
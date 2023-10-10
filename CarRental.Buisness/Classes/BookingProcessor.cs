﻿using CarRental.Common.Classes;
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

	public IEnumerable<IBooking> GetBookings() {

		
		return _db.GetBookings();
		
		
		
	}

/*  public void BookVehicle(IVehicle vehicle, Customer customer)
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


	}

	double CalculateKmCost(IBooking booking, double kmreturned)
	{
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
}
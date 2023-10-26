using CarRental.Data.Interfaces;
using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using CarRental.Common.Classes;
using System.Linq.Expressions;
using System.Reflection;
using CarRental.Common.Extensions;
using System;
using System.Runtime.ConstrainedExecution;

namespace CarRental.Data.Classes;

public class CollectionData : IData
{
	readonly List<IPerson> _persons = new List<IPerson>();
	readonly List<Vehicle> _vehicles = new List<Vehicle>();
	readonly List<IBooking> _bookings = new List<IBooking>();
	LoadData _data;
	public CollectionData(LoadData data)
	{

		_data = data;

	}

	public async Task LoadDataFromJson()
	{
		try
		{
			var resultPersons = await _data.ReadFile<Customer>("sample-data\\customers.json");
			var resultCars = await _data.ReadFile<Car>("sample-data\\cars.json");
			var resultMotorcycles = await _data.ReadFile<Motorcycle>("sample-data\\motorcycles.json");

			foreach (var person in resultPersons)
			{

				_persons.Add((IPerson)person);
				GetNextItemId(_persons, (IPerson)person);
			}

			foreach (var car in resultCars)
			{

				_vehicles.Add((Vehicle)car);
				GetNextItemId(_vehicles, (Vehicle)car);
			}

			foreach (var motorcycle in resultMotorcycles)
			{

				_vehicles.Add((Vehicle)motorcycle);
				GetNextItemId(_vehicles, (Vehicle)motorcycle);
			}
		}
		catch (Exception ex) { }

	}



	public int GetNextItemId<T>(List<T> list, T item) where T : IBase
	{
		int NextId = list.Count.Equals(0) ? 1 : list.Max(b => b.Id) + 1;
		item.Id = NextId;
		return NextId;
	}
	

	private List<T> GetCollection<T>() where T : IBase
	{
		var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
			.FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
			?? throw new InvalidOperationException("Unsupported type");

		var value = collections.GetValue(this) ?? throw new InvalidDataException();

		return ((List<T>)value);
	}

	public List<T> Get<T>(Expression<Func<T, bool>>? expression) where T : IBase
	{
		try
		{
			var collection = GetCollection<T>().AsQueryable();

			if (expression is null) return collection.ToList();

			return collection.Where(expression).ToList();
		}
		catch (Exception ex)
		{
			throw;
		}
	}

	public T? Single<T>(Expression<Func<T, bool>>? expression) where T : IBase
	{
		try
		{
			var collection = GetCollection<T>().AsQueryable();

			if (expression is null) throw new ArgumentNullException(nameof(expression));
			
			var filteredCollection = collection.SingleOrDefault(expression);

			return filteredCollection ?? throw new InvalidOperationException("Multiple matching items found.");
		}
		catch (Exception ex)
		{
			throw;
		}
	}

	public void Add<T>(T item) where T : IBase
	{
		try
		{
			if (item is not null)
			{
				GetCollection<T>().Add(item);
				GetNextItemId(GetCollection<T>(), item);
			}
		}
		catch (Exception ex)
		{
			throw;
		}
	}


	public IBooking RentVehicle(int vehicleId, int customerId)
	{
		var vehicle = _vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
		var customer = _persons.FirstOrDefault(x => x.Id.Equals(customerId));

		if (vehicle == null) throw new ArgumentNullException("Vehicle not found");

		if (customer == null) throw new ArgumentNullException("Customer not found");

		Booking booking = new Booking(vehicle, (Customer)customer);

		_bookings.Add(booking);

		return booking;
	}
	public IBooking ReturnVehicle(int vehicleId, double distance)
	{
		var booking = _bookings.First(x => x.Vehicle.Id.Equals(vehicleId));
		booking.KmReturned = distance + booking.Vehicle.Odometer;
		booking.ReturnedDate = DateTime.Now;
		booking.BookingStatus = false;
		booking.Vehicle.VehicleStatus = VehicleStatuses.Available;
		booking.TotalCost= booking.TotalCost();
		return booking;

	}


}

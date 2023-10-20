using CarRental.Data.Interfaces;
using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using CarRental.Common.Classes;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

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
				person.Id = NextPersonId;
				_persons.Add((IPerson)person);
			}

			foreach (var car in resultCars)
			{
				car.Id = NextVehicleId;
				_vehicles.Add((Vehicle)car);
			}

			foreach (var motorcycle in resultMotorcycles)
			{
				motorcycle.Id = NextVehicleId;
				_vehicles.Add((Vehicle)motorcycle);
			}
		}
		catch (Exception ex) { }

		//Tillfälliga bokningar, ska göras om i VG uppgiften 
		_bookings.Add(new Booking(_vehicles.ElementAt(1), (Customer)_persons.ElementAt(1)));
		_bookings.Add(new Booking(_vehicles.ElementAt(2), (Customer)_persons.ElementAt(2)));


	}
	public IEnumerable<IPerson> GetPersons() => _persons;
	public IEnumerable<IBooking> GetBookings() => _bookings;
	public IEnumerable<Vehicle> GetVehicles(VehicleStatuses status = default) => _vehicles;

	//********************nytt
	public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
	public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(b => b.Id) + 1;
	public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;
	public List<T> Get<T>(Expression<Func<T, bool>>? expression) where T : IBase
	{
		try
		{
			var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
                ?? throw new InvalidOperationException("Unsupported type");

            var value = collections.GetValue(this) ?? throw new InvalidDataException();

            var collection = ((List<T>)value).AsQueryable();

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
			var collections = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
				.FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
				?? throw new InvalidOperationException("Unsupported type");

			var value = collections.GetValue(this) ?? throw new InvalidDataException();

			var collection = ((List<T>)value).AsQueryable();

			if (expression is null)
			{
				throw new ArgumentNullException(nameof(expression));
			}

			var filteredCollection = collection.Where(expression).ToList();
			

			if (filteredCollection.Count == 0)
			{
				throw new InvalidOperationException("No matching elements found.");
			}
			else if (filteredCollection.Count > 1)
			{
				throw new InvalidOperationException("Multiple matching elements found.");
			}

			return filteredCollection.First();
		}
		catch (Exception ex)
		{
			throw;
		}
	}
	public void Add<T>(T item)
	{
		if (item == null)
		{
			throw new ArgumentNullException(nameof(item));
		}

		switch (item)
		{
			case IPerson person:
				person.Id = NextPersonId;
				_persons.Add(person);
				break;
			case Vehicle vehicle:
				vehicle.Id = NextVehicleId;
				_vehicles.Add(vehicle);
				break;
			case IBooking booking:
				booking.Id = NextBookingId;
				_bookings.Add(booking);
				break;
			default:
				throw new ArgumentException("Unsupported item type", nameof(item));
		}
	}

	public IBooking RentVehicle(int vehicleId, int customerId) {  throw new NotImplementedException(); }
	public	IBooking ReturnVehicle(int vehicleId) {  throw new NotImplementedException(); }

	
}

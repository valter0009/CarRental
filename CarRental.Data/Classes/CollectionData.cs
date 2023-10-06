﻿using CarRental.Data.Interfaces;
using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using CarRental.Common.Classes;
using System.Collections.Generic;

namespace CarRental.Data.Classes;

public class CollectionData : IData
{
	readonly List<IPerson> _persons = new List<IPerson>();
	readonly List<IVehicle> _vehicles = new List<IVehicle>();	
	readonly List<IBooking> _bookings = new List<IBooking>();
	LoadData _data;



	public CollectionData(LoadData data)
	{

		_data = data;

	}

	public async Task LoadDataFromJson()
	{
		try { 
		var resultPersons = await _data.ReadFile<Customer>("sample-data\\customers.json");
		_persons.AddRange(resultPersons.OfType<IPerson>().ToList());
		
		var resultCars = await _data.ReadFile<Car>("sample-data\\cars.json");
		_vehicles.AddRange(resultCars.OfType<IVehicle>().ToList());

		var resultMotorcycles = await _data.ReadFile<Motorcycle>("sample-data\\motorcycles.json");
		_vehicles.AddRange(resultMotorcycles.OfType<IVehicle>().ToList());
		} catch { }


	}




	public IEnumerable<IPerson> GetPersons() => _persons;
	public IEnumerable<IBooking> GetBookings() => _bookings;
	public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default) => _vehicles;
}

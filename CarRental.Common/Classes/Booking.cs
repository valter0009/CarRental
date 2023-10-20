using CarRental.Common.Enums;
using CarRental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Common.Classes
{
	public class Booking : IBooking
	{
		public int Id { get; set; }
		public Vehicle Vehicle { get; init; }
		public Customer Customer { get; init; }

		public DateTime RentedDate { get; init; }
		public DateTime ReturnedDate { get; set; }

		public double KmReturned {  get; set; }
		public double TotalCost { get; set; }
		public bool BookingStatus { get; set; } = false;

		public Booking(Vehicle vehicle, Customer customer)
		{
			Vehicle = vehicle;
			Customer = customer;
			RentedDate = DateTime.Now;
			BookingStatus = true;
			vehicle.VehicleStatus = VehicleStatuses.Booked;
		}

		

		
	}
}

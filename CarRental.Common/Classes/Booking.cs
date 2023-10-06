using CarRental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Common.Classes
{
	public class Booking:IBooking
	{
		public IVehicle Vehicle { get; set; }
		public Customer Customer { get; set; }
		public int KmWhenRented { get; set; }
		public int KmWhenReturned { get; set; }
		public DateTime RentedDate { get; set; }
		public DateTime ReturnedDate { get; set; }
		public int Cost { get; set; }
		public bool Status { get; set; } = false;
	}
}

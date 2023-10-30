using CarRental.Common.Classes;

namespace CarRental.Common.Interfaces
{
	public interface IBooking:IBase
	{
	
		bool BookingStatus { get; set; }
		double? TotalCost { get; set; }
		double? KmReturned { get; set; }
		Customer Customer { get; init; }
		DateTime RentedDate { get; init; }
		DateTime ReturnedDate { get; set; } 
		Vehicle Vehicle { get; init; }
	}
}
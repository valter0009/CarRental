using CarRental.Common.Classes;

namespace CarRental.Common.Interfaces;

public interface IBooking: IBase
{
	public IVehicle Vehicle { get; set; }
	public Customer Customer { get; set; }
	public int KmWhenRented { get; set; }
	public int KmWhenReturned { get; set; }
	public DateTime RentedDate { get; set; }
	public DateTime ReturnedDate { get; set; }
	public int Cost { get; set; }
	public bool Status { get; set; }

}

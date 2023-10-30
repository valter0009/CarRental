using CarRental.Common.Classes;
using CarRental.Common.Interfaces;

namespace CarRental.Common.Extensions;

public static class CalculateCost
{
	public static double? TotalCost(this IBooking booking) { 
	var total = CalculateDailyCost(booking, booking.RentedDate, booking.ReturnedDate, booking.Vehicle.DailyCost) + CalculateKmCost(booking, booking.KmReturned);
	return total;
	}
	private static double? CalculateKmCost(IBooking booking, double? kmreturned)
	{
		if (kmreturned is null) { kmreturned = booking.Vehicle.Odometer; }
		double? KmCost = (kmreturned - booking.Vehicle.Odometer) * booking.Vehicle.KmCost;
		return KmCost;
	}
	private static double? CalculateDailyCost(IBooking booking, DateTime rented, DateTime returned, double? dailycost)
	{
		TimeSpan timeDifference = rented - returned;
		int daysDifference = timeDifference.Days;
		if (daysDifference < 1) return dailycost;
		else return dailycost * daysDifference;

	}
}

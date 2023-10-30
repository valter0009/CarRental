namespace CarRental.Common.Interfaces;

public interface IPerson : IBase
{
	
	string FirstName { get; init; }
	string LastName { get; init; }
	int? SocialSecurityNumber { get; init; }
	string FullName => $"{FirstName} {LastName}";
}

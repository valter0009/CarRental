namespace CarRental.Common.Interfaces;

public interface IPerson : IBase
{
	string FirstName { get; set; }
	string LastName { get; set; }
	int SocialSecurityNumber { get; set; }
	string FullName => $"{FirstName} {LastName}";
}

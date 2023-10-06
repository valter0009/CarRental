namespace CarRental.Common.Interfaces;

public interface IPerson: IBase
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int SocialSecurityNumber { get; set; }
}

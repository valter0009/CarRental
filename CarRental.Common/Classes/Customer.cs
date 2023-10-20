using CarRental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Common.Classes;

public class Customer : IPerson
{
	public int Id { get; set;}
	public string FirstName { get; init; }
	public string LastName { get; init; }
	public int SocialSecurityNumber { get; init; }
	public string FullName => $"{FirstName} {LastName}";
	public Customer(string firstname, string lastname, int socialsecuritynumber)
    {
        FirstName = firstname;
        LastName = lastname;
        SocialSecurityNumber = socialsecuritynumber;
    }
}
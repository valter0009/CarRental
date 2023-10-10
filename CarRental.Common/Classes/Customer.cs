using CarRental.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Common.Classes;

public class Customer : IPerson
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int SocialSecurityNumber { get; set; }
	public string FullName => $"{FirstName} {LastName}";
	public Customer(string firstname, string lastname, int socialsecuritynumber)
    {
        FirstName = firstname;
        LastName = lastname;
        SocialSecurityNumber = socialsecuritynumber;
    }
}
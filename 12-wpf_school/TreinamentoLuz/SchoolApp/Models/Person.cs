using System;

namespace SchoolApp.Models
{
	internal class Person
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime BirthDay { get; set; }

		public Person(string firstName, string lastName, DateTime birthDay)
		{
			FirstName = firstName;
			LastName = lastName;
			BirthDay = birthDay;
		}
	}
}

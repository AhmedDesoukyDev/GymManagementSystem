using GymSystemDAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
	public abstract class GymUser : ModelBase
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public Gender Gender { get; set; }
		public Address Address { get; set; }

	}
	[Owned]
	public class Address
	{
		public int BuildingNo { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
	}
}

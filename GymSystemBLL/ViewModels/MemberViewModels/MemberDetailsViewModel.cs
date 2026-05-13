using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.MemberViewModels
{
	public class MemberDetailsViewModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string? Photo { get; set; }
		public string Phone { get; set; }
		public string Gender { get; set; }
		public string DateOfBirth { get; set; }
		public string PlaneName { get; set; }
		public string MembershipStartDate { get; set; }
		public string MembershipEndDate { get; set; }
		public string Address { get; set; }
	
	}
}

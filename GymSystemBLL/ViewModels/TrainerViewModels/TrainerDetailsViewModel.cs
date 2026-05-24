using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.TrainerViewModels
{
	public class TrainerDetailsViewModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Specialization { get; set; }
		[DisplayName("Date Of Birth")]
		public string DateOfBirth { get; set; }
		public string Address { get; set; }

	}
}

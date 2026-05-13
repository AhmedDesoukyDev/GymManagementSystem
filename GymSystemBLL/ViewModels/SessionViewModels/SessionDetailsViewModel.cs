using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.SessionViewModels
{
	public class SessionDetailsViewModel
	{
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public string TrainerName { get; set; }
		public int Capacity { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int AvailableSlots { get; set; }

		#region Computed

		public string Duration => $"{(EndDate - StartDate).Hours} Hours {(EndDate - StartDate).Minutes} Minutes";
		public string Status
		{
			get

			{
				if (StartDate > DateTime.Now)
				{
					return "Upcoming";
				}
				else if (EndDate > DateTime.Now)
				{

					return "Ongoing";
				}
				else
				{
					return "Completed";

				}
			}

		}
		#endregion



	}
}

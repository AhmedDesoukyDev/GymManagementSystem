using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.SessionViewModels
{
	public class UpdateSessionViewModel
	{
		[Required(ErrorMessage = "Trainer is Required")]
		[Display(Name = "Trainer")] //UI
		public int TrainerId { get; set; }

		[Required(ErrorMessage = "Description is Required")]
		[StringLength(500, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 500 chars")]

		public string Description { get; set; }
		[Required(ErrorMessage = "StartDate is required")]
		[Display(Name = "Start Date & Time")] //UI
		[DataType(DataType.DateTime)]//UI
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "EndDate is required")]
		[DataType(DataType.DateTime)]//UI
		[Display(Name = "End Date & Time")] //UI
		public DateTime EndDate { get; set; }
	}
}

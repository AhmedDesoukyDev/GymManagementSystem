using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.PlanViewModels
{
	public class UpdatePlanViewModel
	{
		
		public string Name { get; set; }

		[Required(ErrorMessage = "Description is Required")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 100 chars")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Duration Days is Required")]
		[Range(1,365,ErrorMessage ="Must be Between 1 to 365 days")]
		public int DurationDays { get; set; }
		[Required(ErrorMessage = "Price is Required")]
		[Range(0.1,10000,ErrorMessage ="Price must be between 0.1 to 10000")]
		[Precision(10,2)]
		public decimal Price { get; set; }

	}
}

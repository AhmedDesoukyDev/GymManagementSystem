using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.MemberViewModels
{
	public class HealthRecordViewModel
	{
		[Required(ErrorMessage ="Height is Required")]
		[Range(0.1,300,ErrorMessage ="Height must be greater than 0")]
		public decimal Height { get; set; }
		[Required(ErrorMessage = "Weight is Required")]
		[Range(0.1, 500, ErrorMessage = "Weight must be greater than 0")]
		public decimal Weight { get; set; }
		[Required(ErrorMessage = "BloodType is Required")]
		[StringLength(3,MinimumLength= 1,ErrorMessage ="Blood Type Maximum 3 chars")]
		public string BloodType { get; set; }
		[StringLength(1000, MinimumLength = 1, ErrorMessage = "Maximum Chars is 1000")]

		public string? Note { get; set; }
	}
}

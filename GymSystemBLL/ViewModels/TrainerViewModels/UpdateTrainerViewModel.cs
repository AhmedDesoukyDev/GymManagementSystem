using GymSystemDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.ViewModels.TrainerViewModels
{
	public class UpdateTrainerViewModel
	{
	
		public string Name { get; set; }
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email Format")] //Validation
		[DataType(DataType.EmailAddress)] //UI Hint , Suggestion etc
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 50 chars")]
		[DisplayName("Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone Number Is Required")]
		[DataType(DataType.PhoneNumber)]
		[Phone(ErrorMessage = "Invalid Phone Format")]
		[RegularExpression(@"^(010|011|015|012)\d{8}$", ErrorMessage = "Phone must be valid Egyptian number")]

		public string Phone { get; set; }
		[Required(ErrorMessage = "Date of birth Is Required")]
		[DataType(DataType.Date)]
		public DateOnly DateOfBirth { get; set; }
		[Required(ErrorMessage = "Gender Is Required")]
		public Gender Gender { get; set; }

		[Required(ErrorMessage = "Building Number Is Required")]
		[Range(1, 9000, ErrorMessage = "Building number between 1 and 9000")]
		public int BuildingNumber { get; set; }

		[Required(ErrorMessage = "Street Is Required")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Street Name must be between 2 and 30 chars")]
		public string Street { get; set; }
		[Required(ErrorMessage = "City Is Required")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 chars")]
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Contains Only letters")]
		public string City { get; set; }
		[Required(ErrorMessage = "Specialization Is Required")]
		public Specialties Specialization { get; set; }
	}
}

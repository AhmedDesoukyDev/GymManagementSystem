using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;

namespace GymSystemPL.Controllers
{
	public class MemberController : Controller
	{
		private readonly IMemberService _memberService;

		public MemberController(IMemberService memberService)
		{
			_memberService = memberService;
		}
		//Get All Members
		public ActionResult Index()
		{
			var members= _memberService.GetAllMembers();
			return View(members);
		}

		//Get Member Details
		public ActionResult MemberDetails(int id)
		{
			//validation incase he sent id from the url not the dropdown in index
			if (id <= 0)
			{
				//Send data to another request
				TempData["ErrorMessage"] = "Invalid Member Id";
				return RedirectToAction(nameof(Index));
			}
			var member = _memberService.GetMemberDetails(id);

			if (member is null)
			{
				TempData["ErrorMessage"] = "Member is not found";
				return RedirectToAction(nameof(Index));
			}
			return View(member);

		}

		//Get Health Record Details
		public ActionResult HealthRecordDetails(int id)
		{
			if (id <= 0)
			{
				TempData["ErrorMessage"] = "Invalid Member Id";
				return RedirectToAction(nameof(Index));
			}
			var healthRecord=_memberService.GetHealthRecordDetails(id);
			if (healthRecord is null)
			{
				TempData["ErrorMessage"] = "Health Record of this member is not found";
				return RedirectToAction(nameof(Index));
			}
			return View(healthRecord);

		}


		//Create Member
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateMember(CreateMemberViewModel createdMember)
		{
			//Check state of the model came from the form
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Invalid Model", "There's something missing in inputs");
				return View(nameof(Create),createdMember);
			
			}

			bool isCreated = _memberService.CreateMember(createdMember);
			if (isCreated)
			{
				TempData["SuccessMessage"] = "Member is Created Successfully";


			}
			else
			{
				TempData["ErrorMessage"] = "Email or Phone number is already exist";


			}
			return RedirectToAction(nameof(Index));


		}
	}
}

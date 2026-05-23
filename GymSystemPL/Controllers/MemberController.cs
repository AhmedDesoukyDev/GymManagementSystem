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


		//Edit Member
		public ActionResult Edit(int id)
		{
			if(id<= 0)
			{
				TempData["ErrorMessage"] = "Invalid Id";
				return RedirectToAction(nameof(Index));
			}
			var MemberToUpdate = _memberService.GetMemberToUpdate(id);
			if (MemberToUpdate is null)
			{
				TempData["ErrorMessage"] = "Member is not found";
				return RedirectToAction(nameof(Index));

			}
			return View(MemberToUpdate);



		}
		[HttpPost]
		//To make sure the id will be from route , so that client cant add it as input in form for different member
		public ActionResult Edit([FromRoute]int id, UpdatedMemberViewModel updatedMember)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("InvalidModel", "Check for missing inputs");
				return View(nameof(Edit),updatedMember);
			}
			bool isUpdated = _memberService.UpdateMember(id, updatedMember);
			if (isUpdated) {

				TempData["SuccessMessage"] = "Member is updated Successfully";
			}
			else
			{
				TempData["ErrorMessage"] = "Updated is Failed , Check Phone or Email";
			}
			return RedirectToAction(nameof(Index));

		}



		//Delete Member
		public ActionResult Delete(int id)
		{
			if (id <= 0)
			{
				TempData["ErrorMessage"] = "Invalid Id";
				return RedirectToAction(nameof(Index));
			}
			var memberToDelete = _memberService.GetMemberDetails(id);
			if (memberToDelete is null)
			{
				TempData["ErrorMessage"] = "Member is not found";
				return RedirectToAction(nameof(Index));

			}
			return View();
		}
		[HttpPost]
		[ActionName("Delete")]
		public ActionResult DeleteConfirmed([FromRoute]int id)
		{
			var result=_memberService.DeleteMember(id);
			if (result)
			{
				TempData["SuccessMessage"] = "Member is Deleted Successfully";
			}
			else
			{
				TempData["ErrorMessage"] = "Member has active sessions , cant be deleted";
			}

			return RedirectToAction(nameof(Index));
		
			

		}
	}

	
	
}

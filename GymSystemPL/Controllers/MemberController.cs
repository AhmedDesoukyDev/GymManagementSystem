using GymSystemBLL.Services.Interfaces;
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
				return RedirectToAction(nameof(Index));
			var member = _memberService.GetMemberDetails(id);
			if (member is null) return RedirectToAction(nameof(Index));

			return View(member);

		}

		//Get Health Record Details
		public ActionResult HealthRecordDetails(int id)
		{
			if (id <= 0) return RedirectToAction(nameof(Index));
			var healthRecord=_memberService.GetHealthRecordDetails(id);
			if (healthRecord is null) return RedirectToAction(nameof(Index));
			return View(healthRecord);

		}
	}
}

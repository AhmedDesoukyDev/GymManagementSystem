using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
	}
}

using GymSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAnalyticsService _analyticsService;

		public HomeController(IAnalyticsService analyticsService)
		{
			_analyticsService = analyticsService;
		}
		public	ActionResult Index()
		{
			var model = _analyticsService.GetAnalytics();
			return View(model);
		}
	}
}

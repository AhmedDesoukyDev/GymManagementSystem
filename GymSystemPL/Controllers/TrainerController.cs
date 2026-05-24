using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemPL.Controllers
{
	public class TrainerController : Controller
	{
		private readonly ITrainerService _trainerService;

		public TrainerController(ITrainerService trainerService)
		{
			_trainerService = trainerService;
		}
		public ActionResult Index()
		{
			var trainers=_trainerService.GetAllTrainers();
			return View(trainers);
		}
		public ActionResult TrainerDetails(int id)
		{
			if (id <= 0)
			{
				TempData["ErrorMessage"] = "Invalid Id , Check the id of the member again";
				return RedirectToAction(nameof(Index));
			}
			var trainer=_trainerService.GetTrainerDetails(id);
			if(trainer is null)
			{
				TempData["ErrorMessage"] = "this trainer is not found";
				return RedirectToAction(nameof(Index));
			}

			return View(trainer);
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(CreatedTrainerViewModel createdTrainer)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("Invalid Model", "Something is missing in inputs");
				return View(createdTrainer);
			}
			bool isCreated = _trainerService.CreateTrainer(createdTrainer);
			if (isCreated)
			{
				TempData["SuccessMessage"] = "Trainer is Created Successfully";
			}
			else
			{
				TempData["ErrorMessage"] = "Trainer isnt Created , Try Another Phone or Email";
			}
			return RedirectToAction(nameof(Index));
		}

		public ActionResult Edit(int id)
		{
			if (id <= 0)
			{
				TempData["ErrorMessage"] = "Invalid Id";
				return RedirectToAction(nameof(Index));
			}
			var trainer=_trainerService.GetUpdatedTrainer(id);
			if(trainer is null)
			{
				TempData["ErrorMessage"] = "This Trainer is not found";
				return RedirectToAction(nameof(Index));
			}

			return View(trainer);

		}

		[HttpPost]
		public ActionResult Edit([FromRoute]int id,UpdateTrainerViewModel trainerToUpdate)
		{
			if(!ModelState.IsValid)
			{
				ModelState.AddModelError("InvalidModel", "Something is missing for inputs , check again");
				return View(trainerToUpdate);
			}
			bool isUpdated=_trainerService.UpdateTrainer(id, trainerToUpdate);
			if (isUpdated)
			{
				TempData["SuccessMessage"] = "Trainer is Updated Successfully";

			}
			else
			{
				TempData["ErrorMessage"] = "An Error Occurred While Updating , Check Phone And Email";
			}
			return RedirectToAction(nameof(Index));


		}

		public ActionResult Delete(int id) 
		{
			if (id <= 0)
			{
				TempData["ErrorMessage"] = "Invalid Id";
				return RedirectToAction(nameof(Index));
			}
			var trainer=_trainerService.GetTrainerDetails(id);
			if (trainer is null)
			{
				TempData["ErrorMessage"] = "This Trainer is not found";
				return RedirectToAction(nameof(Index));
			}
			ViewBag.TrainerId=id;
			ViewBag.TrainerName = trainer.Name;

			return View();
			

		}

		[HttpPost]
		public ActionResult DeleteConfirmed([FromForm]int id)
		{
			var isDeleted = _trainerService.DeleteTrainer(id);
			if (isDeleted)
			{
				TempData["SuccessMessage"] = "Trainer is Removed Successfully";
			}
			else
			{
				TempData["ErrorMessage"] = "Trainer is'nt Deleted";

			}
			return RedirectToAction(nameof(Index));
		}
	}
}

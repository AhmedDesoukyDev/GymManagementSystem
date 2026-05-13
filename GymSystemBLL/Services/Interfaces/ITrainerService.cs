using GymSystemBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
	public interface ITrainerService
	{
		IEnumerable<TrainerViewModel> GetAllTrainers();

		bool CreateTrainer(CreatedTrainerViewModel model);

		TrainerDetailsViewModel? GetTrainerDetails(int id);

		UpdateTrainerViewModel? GetUpdatedTrainer(int id);
		bool UpdateTrainer(int id, UpdateTrainerViewModel updatedTrainer);

		bool DeleteTrainer(int id);

	}
}

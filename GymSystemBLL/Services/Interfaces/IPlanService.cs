using GymSystemBLL.ViewModels.PlanViewModels;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
	public interface IPlanService
	{
		IEnumerable<PlanViewModel> GetPlans();
		PlanViewModel? GetPlanById(int id);

		UpdatePlanViewModel? GetUpdatedPlan(int id);

		bool UpdatePlan(int id, UpdatePlanViewModel plan);

		bool ToggleStatus(int planId);
		

	}
}

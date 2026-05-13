using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.PlanViewModels;
using GymSystemDAL.Data.UnitOfWork;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Classes
{
	public class PlanService : IPlanService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PlanService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public PlanViewModel? GetPlanById(int id)
		{
			var result = _unitOfWork.GetRepository<Plan>().GetById(id);
			if (result == null) return null;
			return new PlanViewModel
			{
				Name = result.Name,
				Description = result.Description,
				IsActive = result.isActive,
				DurationDays = result.DurationDays,
				Price = result.Price

			};
		}

		public IEnumerable<PlanViewModel> GetPlans()
		{
			var result = _unitOfWork.GetRepository<Plan>().GetAll();
			if (result is null || !result.Any()) return [];

			var members = result.Select(X => new PlanViewModel
			{
				Id = X.Id,
				Name = X.Name,
				Description = X.Description,
				DurationDays = X.DurationDays,
				Price = X.Price,
				IsActive = X.isActive

			});

			return members;
		}

		public UpdatePlanViewModel? GetUpdatedPlan(int id)
		{
			var updatedPlan = _unitOfWork.GetRepository<Plan>().GetById(id);
			if (updatedPlan is null || updatedPlan.isActive==false || hasActiveMembership(id)) return null;
			return new UpdatePlanViewModel
			{
				Name = updatedPlan.Name,
				Description = updatedPlan.Description,
				DurationDays = updatedPlan.DurationDays,
				Price = updatedPlan.Price,
			};
		}

		public bool ToggleStatus(int planId)
		{
			var Repo = _unitOfWork.GetRepository<Plan>();
			var plan=	Repo.GetById(planId);
			if(plan is null || hasActiveMembership(planId)) return false;

			if (plan.isActive)
				plan.isActive = false;
			else plan.isActive = true;

			//Everytime i update i must update the updateat property
			plan.UpdatedAt=DateTime.Now;
			try
			{
				Repo.Update(plan);
				return _unitOfWork.Complete() > 0;
			}
			catch (Exception)
			{

				return false;
			}
			

		}

		public bool UpdatePlan(int id, UpdatePlanViewModel Updatedplan)
		{
			try
			{
				var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
				if (plan is null || hasActiveMembership(id)) return false;

				(plan.Description,plan.DurationDays,plan.Price ,plan.UpdatedAt)=
					(Updatedplan.Description,Updatedplan.DurationDays,Updatedplan.Price,DateTime.Now);



				_unitOfWork.GetRepository<Plan>().Update(plan);
				return _unitOfWork.Complete() > 0;

			}
			catch (Exception )
			{
				return false;
				
			}
		}

		#region Helper Methods

		private bool hasActiveMembership(int id)
		{
			return _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.PlanId == id && X.Status == "Active").Any();
		}
		#endregion
	}
}

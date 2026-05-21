using AutoMapper;
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
		private readonly IMapper _mapper;

		public PlanService(IUnitOfWork unitOfWork,IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public PlanViewModel? GetPlanById(int id)
		{
			var result = _unitOfWork.GetRepository<Plan>().GetById(id);
			if (result == null) return null;
			return _mapper.Map<PlanViewModel>(result);
		}

		public IEnumerable<PlanViewModel> GetPlans()
		{
			var result = _unitOfWork.GetRepository<Plan>().GetAll();
			if (result is null || !result.Any()) return [];

			var members = _mapper.Map<IEnumerable<PlanViewModel>>(result);

			return members;
		}

		public UpdatePlanViewModel? GetUpdatedPlan(int id)
		{
			var updatedPlan = _unitOfWork.GetRepository<Plan>().GetById(id);
			if (updatedPlan is null || updatedPlan.isActive==false || hasActiveMembership(id)) return null;
			return _mapper.Map<UpdatePlanViewModel>(updatedPlan);
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
				//Tuple
				_mapper.Map(Updatedplan,plan);



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

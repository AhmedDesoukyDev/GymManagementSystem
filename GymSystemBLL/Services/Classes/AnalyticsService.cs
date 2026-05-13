using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.AnalyticsViewModels;
using GymSystemDAL.Data.UnitOfWork;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Classes
{
	public class AnalyticsService : IAnalyticsService
	{
		private readonly IUnitOfWork _unitOfWork;

		public AnalyticsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public AnalyticsViewModel GetAnalytics()
		{
			var sessions=_unitOfWork.GetRepository<Session>().GetAll();
			var Members = _unitOfWork.GetRepository<Member>().GetAll().Count();
			var ActiveMembers = _unitOfWork.GetRepository<MemberShip>().GetAll().Count(X=>X.Status=="Active");
			var UpcomingSessions = sessions.Count(X=>X.StartDate > DateTime.Now);
			var OngoingSessions = sessions.Count(X=>X.StartDate <= DateTime.Now && X.EndDate >DateTime.Now);
			var CompletedSessions = sessions.Count(X=>X.EndDate  <= DateTime.Now);
			var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count();
			return new AnalyticsViewModel
			{
				ActiveMembers = ActiveMembers,
				TotalMembers = Members,
				CompletedSessions= CompletedSessions,
				OngoingSessions= OngoingSessions,
				UpcomingSessions= UpcomingSessions,
				Trainers=Trainers
			};
		}
	}
}

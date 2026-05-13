using AutoMapper;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.SessionViewModels;
using GymSystemDAL.Data.UnitOfWork;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Classes
{
	public class SessionService : ISessionService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public bool CreateSession(CreateSessionViewModel createdSession)
		{
			if (createdSession is null) return false;
			var trainerValid = isTrainerValid(createdSession.TrainerId);
			var categoryValid = isCategoryValid(createdSession.CategoryId);
			var capacityValid = isCapacityValid(createdSession.Capacity);
			var dateCheck = isDateValid(createdSession.StartDate, createdSession.EndDate);
			if (!(trainerValid && categoryValid && capacityValid && dateCheck)) return false;
			var session = _mapper.Map<Session>(createdSession);

			try
			{
				_unitOfWork.SessionRepository.Add(session);
				return _unitOfWork.Complete() > 0;

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Creating of Session is Failed , {ex}");
				return false;
			}
		}

		public IEnumerable<SessionViewModel> GetAll()
		{
			var sessions = _unitOfWork.SessionRepository.GetSessionsWithTrainersAndCategories();
			if (sessions is null || !sessions.Any()) return [];

			var mappedSessions = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

			foreach (var session in mappedSessions)
				session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.CountOfBookedSlots(session.Id);
			return mappedSessions;


		}

		public SessionDetailsViewModel? GetById(int id)
		{
			var result = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(id);
			if (result is null) return null;

			var session = _mapper.Map<SessionDetailsViewModel>(result);
			session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.CountOfBookedSlots(id);
			return session;
		}
		public UpdateSessionViewModel? GetSessionToUpdate(int id)
		{
			var session = _unitOfWork.SessionRepository.GetById(id);

			if (!isSessionAvailableToUpdate(session!)) return null;

			var sessionToUpdate = _mapper.Map<UpdateSessionViewModel>(session);
			return sessionToUpdate;
		}

		public bool UpdateSession(int id, UpdateSessionViewModel updateSession)
		{


			try
			{
				var session = _unitOfWork.SessionRepository.GetById(id);
				if (!isSessionAvailableToUpdate(session!)) return false;
				if (isTrainerValid(updateSession.TrainerId)) return false;
				if (!isDateValid(updateSession.StartDate, updateSession.EndDate)) return false;

				_mapper.Map<Session>(updateSession);
				session!.UpdatedAt = DateTime.Now;

				_unitOfWork.SessionRepository.Update(session);

				return _unitOfWork.Complete() > 0;
			}
			catch (Exception ex)
			{

				Console.WriteLine($"Failed to update , {ex}");
				return false;
			}


		}
		public bool DeleteSession(int id)
		{

			try
			{
				var session = _unitOfWork.SessionRepository.GetById(id);
				if (!isSessionAvailableToDelete(session!)) return false;
				_unitOfWork.SessionRepository.Delete(session!);
				return _unitOfWork.Complete() > 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Can not delete the session , {ex}");
				return false;
			}
		}

		#region Helper Method
		private bool isSessionAvailableToUpdate(Session session)
		{
			//Future with no booking
			if (session is null) return false;
			return session.StartDate > DateTime.Now &&
			 _unitOfWork.SessionRepository.CountOfBookedSlots(session.Id) == 0;


		}
		private bool isSessionAvailableToDelete(Session session)
		{
			if (session is null) return false;
			//Completed with no booking
			return session.EndDate < DateTime.Now &&
			_unitOfWork.SessionRepository.CountOfBookedSlots(session.Id) == 0;




		}
		private bool isTrainerValid(int trainerId) => _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
		private bool isCategoryValid(int CategoryId) => _unitOfWork.GetRepository<Category>().GetById(CategoryId) is not null;
		private bool isDateValid(DateTime startDate, DateTime endDate) => startDate < endDate;
		private bool isCapacityValid(int capacity) => capacity >= 1 && capacity <= 25;




		#endregion
	}
}

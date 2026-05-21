using AutoMapper;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.TrainerViewModels;
using GymSystemDAL.Data.UnitOfWork;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Classes
{
	public class TrainerService : ITrainerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public TrainerService(IUnitOfWork unitOfWork,IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public bool CreateTrainer(CreatedTrainerViewModel model)
		{
			if(model is null) return false;
			if (EmailCheck(model.Email) || PhoneCheck(model.Phone)) return false;
			//if(model.Specialization<=0)
			var trainer = _mapper.Map<Trainer>(model);

			try
			{
				_unitOfWork.GetRepository<Trainer>().Add(trainer);
				return _unitOfWork.Complete() > 0;
			}
			catch (Exception)
			{

				return false;
			}

			

		}

		public bool DeleteTrainer(int id)
		{
			var result = _unitOfWork.GetRepository<Trainer>().GetById(id);
			if (result == null) return false;
			var sessionsOfTrainer = _unitOfWork.GetRepository<Session>().
				GetAll(X=>X.TrainerId==id && X.StartDate > DateTime.Now).FirstOrDefault();
			if (sessionsOfTrainer is not null) return false;

			try
			{
				_unitOfWork.GetRepository<Trainer>().Delete(result);

				return _unitOfWork.Complete() > 0;

			}
			catch (Exception)
			{

				return false;
			}

		}

		public IEnumerable<TrainerViewModel> GetAllTrainers()
		{
			var result = _unitOfWork.GetRepository<Trainer>().GetAll();
			if (result is null || !result.Any()) return [];
			var trainers =_mapper.Map<IEnumerable<TrainerViewModel>>(result);
			return trainers;

		}

		public TrainerDetailsViewModel? GetTrainerDetails(int id)
		{
			var result = _unitOfWork.GetRepository<Trainer>().GetById(id);
			if(result is null) return null;
			return _mapper.Map<TrainerDetailsViewModel>(result);
		}

		public UpdateTrainerViewModel? GetUpdatedTrainer(int id)
		{
			var result = _unitOfWork.GetRepository<Trainer>().GetById(id);
			if (result is null) return null;

			return _mapper.Map<UpdateTrainerViewModel>(result);
		}

		public bool UpdateTrainer(int id, UpdateTrainerViewModel updatedTrainer)
		{
			if(updatedTrainer == null) return false;
			if (EmailCheck(updatedTrainer.Email) || PhoneCheck(updatedTrainer.Phone)) return false;
			var result = _unitOfWork.GetRepository<Trainer>().GetById(id);
			if (result is null) return false;
			try
			{
				_mapper.Map(updatedTrainer, result);

				_unitOfWork.GetRepository<Trainer>().Update(result);
				return _unitOfWork.Complete() > 0;
			}
			catch (Exception)
			{

				return false;
			}

		}
		#region Helper Methods

		private bool EmailCheck(string email)
		{
			return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == email).Any();
			
		}
		private bool PhoneCheck(string Phone)
		{
			return _unitOfWork.GetRepository<Trainer>().GetAll(X => X.Email == Phone).Any();

		}
		#endregion
	}
}

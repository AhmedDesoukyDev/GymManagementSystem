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

		public TrainerService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public bool CreateTrainer(CreatedTrainerViewModel model)
		{
			if(model is null) return false;
			if (EmailCheck(model.Email) || PhoneCheck(model.Phone)) return false;
			//if(model.Specialization<=0)
			var trainer = new Trainer
			{
				Name = model.Name,
				Email = model.Email,
				PhoneNumber = model.Phone,
				DateOfBirth = model.DateOfBirth,
				Gender = model.Gender,
				Specialties=model.Specialization,
				Address = new Address
				{
					BuildingNo = model.BuildingNumber,
					Street = model.Street,
					City = model.City,
				},
				
			};

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
			var trainers = result.Select(X => new TrainerViewModel
			{
				Id = X.Id,
				Name = X.Name,
				Email = X.Email,
				Phone = X.PhoneNumber,
				Specialization = X.Specialties.ToString(),
			});
			return trainers;

		}

		public TrainerDetailsViewModel? GetTrainerDetails(int id)
		{
			var result = _unitOfWork.GetRepository<Trainer>().GetById(id);
			if(result is null) return null;
			return new TrainerDetailsViewModel
			{
				Name = result.Name,
				Email = result.Email,
				Phone = result.PhoneNumber,
				Specialization = result.Specialties.ToString(),
				DateOfBirth=result.DateOfBirth.ToShortDateString(),
				Address = $"{result.Address.BuildingNo} - {result.Address.Street} - {result.Address.City}"
			};
		}

		public UpdateTrainerViewModel? GetUpdatedTrainer(int id)
		{
			var result = _unitOfWork.GetRepository<Trainer>().GetById(id);
			if (result is null) return null;

			return new UpdateTrainerViewModel
			{
				Name = result.Name,
				Email = result.Email,
				Phone = result.PhoneNumber,
				BuildingNumber = result.Address.BuildingNo,
				City = result.Address.City,
				Street = result.Address.Street,
				Specialization = result.Specialties,
			};
		}

		public bool UpdateTrainer(int id, UpdateTrainerViewModel updatedTrainer)
		{
			if(updatedTrainer == null) return false;
			if (EmailCheck(updatedTrainer.Email) || PhoneCheck(updatedTrainer.Phone)) return false;
			var result = _unitOfWork.GetRepository<Trainer>().GetById(id);
			if (result is null) return false;
			try
			{
				result.Email= updatedTrainer.Email;
				result.PhoneNumber = updatedTrainer.Phone;
				result.Address.BuildingNo = updatedTrainer.BuildingNumber;
				result.Address.City=updatedTrainer.City;
				result.Address.Street=updatedTrainer.Street;
				result.Specialties = updatedTrainer.Specialization;
				result.UpdatedAt=DateTime.Now;

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

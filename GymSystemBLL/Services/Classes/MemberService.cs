using AutoMapper;
using GymSystemBLL.Services.Interfaces;
using GymSystemBLL.ViewModels.MemberViewModels;
using GymSystemDAL.Data.UnitOfWork;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Classes
{
	public class MemberService : IMemberService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public MemberService(IUnitOfWork unitOfWork,IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}



		public IEnumerable<MemberViewModel> GetAllMembers()
		{
			var result = _unitOfWork.GetRepository<Member>().GetAll();
			if (result is null || !result.Any()) return []; //Empty Collection
			var members = _mapper.Map<IEnumerable<MemberViewModel>>(result);
			return members;

		}



		public bool CreateMember(CreateMemberViewModel createMemberViewModel)
		{

			try
			{
				if (createMemberViewModel is null || isEmailExist(createMemberViewModel.Email) || isPhoneExist(createMemberViewModel.Email)) return false;

				var newMember = _mapper.Map<Member>(createMemberViewModel);

				//var newMember = new Member
				//{
				//	Name = createMemberViewModel.Name,
				//	DateOfBirth = createMemberViewModel.DateOfBirth,
				//	Email = createMemberViewModel.Email,
				//	Gender = createMemberViewModel.Gender,
				//	PhoneNumber = createMemberViewModel.Phone,
				//	HealthRecord = new HealthRecord
				//	{
				//		Height = createMemberViewModel.HealthRecordViewModel.Height,
				//		Notes = createMemberViewModel.HealthRecordViewModel.Note,
				//		Weight = createMemberViewModel.HealthRecordViewModel.Weight,
				//		BloodType = createMemberViewModel.HealthRecordViewModel.BloodType

				//	},
				//	Address = new Address
				//	{
				//		BuildingNo = createMemberViewModel.BuildingNumber,
				//		City = createMemberViewModel.City,
				//		Street = createMemberViewModel.Street,
				//	}

				//};

				_unitOfWork.GetRepository<Member>().Add(newMember);

				return _unitOfWork.Complete() > 0;
			}
			catch (Exception)
			{

				return false;
			}
			

		}

		public MemberDetailsViewModel? GetMemberDetails(int id)
		{
			var result = _unitOfWork.GetRepository<Member>().GetById(id);
			if (result == null) return null;

			var memberDetails = _mapper.Map<MemberDetailsViewModel>(result);
			var memberShip = _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.MemberId == id).Where(X=>X.Status == "Active").FirstOrDefault();
			if (memberShip is not null)
			{
				memberDetails.MembershipStartDate = memberShip.CreatedAt.ToShortDateString();
				memberDetails.MembershipEndDate = memberShip.EndDate.ToShortDateString();
				var plan = _unitOfWork.GetRepository<Plan>().GetById(memberShip.PlanId);
				memberDetails.PlanName = plan?.Name;
			}

			return memberDetails;


		}

		public HealthRecordViewModel? GetHealthRecordDetails(int id)
		{
			var healthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(id);
			if (healthRecord is null) return null;
			return _mapper.Map<HealthRecordViewModel>(healthRecord);

		}

		public UpdatedMemberViewModel? GetMemberToUpdate(int id)
		{
			var memberToUpdate= _unitOfWork.GetRepository<Member>().GetById(id);
			if (memberToUpdate is null) return null;
			return _mapper.Map<UpdatedMemberViewModel>(memberToUpdate);
		}

		public bool UpdateMember(int id,UpdatedMemberViewModel updateMemberViewModel)
		{
			try
			{
				if (updateMemberViewModel == null || isEmailExist(updateMemberViewModel.Email) || isPhoneExist(updateMemberViewModel.Phone)) return false;

				var result = _unitOfWork.GetRepository<Member>().GetById(id);
				if (result is not null)
				{	
					_mapper.Map(updateMemberViewModel, result);
					
					_unitOfWork.GetRepository<Member>().Update(result);
					return _unitOfWork.Complete() > 0;
				}
				return false;
			}
			catch (Exception)
			{

				return false;
			}

			



		}

		public bool DeleteMember(int id)
		{
			var memberToDelete = _unitOfWork.GetRepository<Member>().GetById(id);
			if(memberToDelete is null) return false;
			var HasActiveSessions = _unitOfWork.GetRepository<MemberSessions>().GetAll(X=>X.MemberId ==id && X.Session.StartDate >DateTime.Now).Any();
			if(HasActiveSessions) return false;

			//On Delete Cascade
			var MemberShips = _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.MemberId == id);
			try
			{
				if (MemberShips.Any())
				{
					foreach (var membership in MemberShips)
					{
						_unitOfWork.GetRepository<MemberShip>().Delete(membership);
					}
					
				}
				_unitOfWork.GetRepository<Member>().Delete(memberToDelete);

				return _unitOfWork.Complete() > 0;
				
				

			}
			catch (Exception)
			{

				return false;
			}

		}

		private bool isEmailExist(string email) => _unitOfWork.GetRepository<Member>().GetAll(X=>X.Email== email).Any();
		private bool isPhoneExist(string phoneNumber) => _unitOfWork.GetRepository<Member>().GetAll(X=>X.PhoneNumber == phoneNumber).Any();

	
	}
}

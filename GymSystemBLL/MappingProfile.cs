using AutoMapper;
using GymSystemBLL.ViewModels.MemberViewModels;
using GymSystemBLL.ViewModels.PlanViewModels;
using GymSystemBLL.ViewModels.SessionViewModels;
using GymSystemBLL.ViewModels.TrainerViewModels;
using GymSystemDAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			MapSession();
			MapMember();
			MapTrainer();
			MapPlan();
		}

		private void MapTrainer()
		{
			CreateMap<Trainer, TrainerViewModel>()
			.ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialties.ToString()));
			CreateMap<CreatedTrainerViewModel, Trainer>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
				{
					BuildingNo = src.BuildingNumber,
					City = src.City,
					Street = src.Street,
				}));
				

			CreateMap<Trainer, TrainerDetailsViewModel>()
				.ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth.ToShortDateString()))
				.ForMember(dest => dest.Specialization, opts => opts.MapFrom(src => src.Specialties.ToString()))
				.ForMember(dest => dest.Address, opts => opts.MapFrom(src => $"{src.Address.BuildingNo} - {src.Address.Street} - {src.Address.City}"));


			CreateMap<Trainer, UpdateTrainerViewModel>()
				.ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Address.Street))
				.ForMember(dest => dest.BuildingNumber, opts => opts.MapFrom(src => src.Address.BuildingNo))
				.ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Address.City));

			CreateMap<UpdateTrainerViewModel, Trainer>()
				.ForMember(dest => dest.Name, opts => opts.Ignore())
				//i dont want it to create a new address object , i want the same one for member to be modified
				.AfterMap((src, dest) =>
				{
					dest.Address.BuildingNo = src.BuildingNumber;
					dest.Address.City = src.City;
					dest.Address.Street = src.Street;
					dest.UpdatedAt = DateTime.Now;
				});

		}
		private void MapMember()
		{

			CreateMap<Member, MemberViewModel>()
				.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
				.ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.PhoneNumber));
			CreateMap<CreateMemberViewModel, Member>()
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
				{
					BuildingNo = src.BuildingNumber,
					City = src.City,
					Street = src.Street,
				}))
				.ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));

			CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

			CreateMap<Member, MemberDetailsViewModel>()
				.ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth.ToShortDateString()))
				.ForMember(dest => dest.Address, opts => opts.MapFrom(src => $"{src.Address.BuildingNo} - {src.Address.Street} - {src.Address.City}"))
				.ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.PhoneNumber));



			CreateMap<Member, UpdatedMemberViewModel>()
				.ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Address.Street))
				.ForMember(dest => dest.BuildingNumber, opts => opts.MapFrom(src => src.Address.BuildingNo))
				.ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Address.City));

			CreateMap<UpdatedMemberViewModel, Member>()
				.ForMember(dest => dest.Name, opts => opts.Ignore()).
				ForMember(dest => dest.Photo, opts => opts.Ignore())
				//i dont want it to create a new address object , i want the same one for member to be modified
				.AfterMap((src, dest) =>
				{
					dest.Address.BuildingNo = src.BuildingNumber;
					dest.Address.City = src.City;
					dest.Address.Street = src.Street;
					dest.UpdatedAt = DateTime.Now;
				});
		}
		private void MapSession()
		{
			CreateMap<Session, SessionDetailsViewModel>()
				//destination member ,  options for that member ( what you want to do )
				.ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.SessionTrainer.Name))
				.ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.SessionCategory.Name))
				.ForMember(dest => dest.AvailableSlots, options => options.Ignore());



			
			CreateMap<Session, SessionViewModel>()
				//destination member ,  options for that member ( what you want to do )
				.ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.SessionTrainer.Name))

				.ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.SessionCategory.Name))

				.ForMember(dest => dest.AvailableSlots, options => options.Ignore());



			CreateMap<CreateSessionViewModel, Session>();

		
		
			CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
		}
		private void MapPlan()
		{
			CreateMap<Plan, PlanViewModel>();

			CreateMap<Plan, UpdatePlanViewModel>();
			CreateMap<UpdatePlanViewModel, Plan>()
		   .ForMember(dest => dest.Name, opt => opt.Ignore())
		   .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
		}
	}
}

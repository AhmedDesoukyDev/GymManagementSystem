using AutoMapper;
using GymSystemBLL.ViewModels.SessionViewModels;
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
			#region Session -- SessionDetailsViewModel
			CreateMap<Session, SessionDetailsViewModel>()
				//destination member ,  options for that member ( what you want to do )
				.ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.SessionTrainer.Name));
			CreateMap<Session, SessionDetailsViewModel>()

				.ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.SessionCategory.Name));
			CreateMap<Session, SessionDetailsViewModel>()

				.ForMember(dest => dest.AvailableSlots, options => options.Ignore());

			#endregion

			#region Session - SessionViewModel
			CreateMap<Session, SessionViewModel>()
				//destination member ,  options for that member ( what you want to do )
				.ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.SessionTrainer.Name));
			CreateMap<Session, SessionViewModel>()

				.ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.SessionCategory.Name));
			CreateMap<Session, SessionViewModel>()

				.ForMember(dest => dest.AvailableSlots, options => options.Ignore());


			#endregion

			#region Session - CreatedSessionViewModel

			CreateMap<CreateSessionViewModel, Session>();

			#endregion


			#region Session - UpdateSessionViewModel

			CreateMap<Session, UpdateSessionViewModel>().ReverseMap();

			#endregion
		}
	}
}

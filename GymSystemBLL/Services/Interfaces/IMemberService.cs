using GymSystemBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemBLL.Services.Interfaces
{
	public interface IMemberService
	{
		 IEnumerable<MemberViewModel> GetAllMembers();
		bool CreateMember(CreateMemberViewModel createMemberViewModel);
		MemberDetailsViewModel? GetMemberDetails(int id);
		HealthRecordViewModel? GetHealthRecordDetails(int id);
		UpdatedMemberViewModel? GetMemberToUpdate(int id);
		bool UpdateMember(int id, UpdatedMemberViewModel updateMemberViewModel);
		bool DeleteMember(int id);
	}
}

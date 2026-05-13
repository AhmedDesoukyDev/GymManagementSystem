using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
	public class Member:GymUser
	{
		//Renaming for column in Member
		//JoinedAt == CreatedAt From ModelBase --- Fluent Api -- Derived Attribute

		public string? Photo { get; set; } //Url Only of photo	

		public virtual ICollection<MemberSessions> MemberSessions { get; set; } = null!;
		public virtual ICollection<MemberShip> Memberships { get; set; } = null!;
		public virtual HealthRecord HealthRecord { get; set; } = null!;
	}
}

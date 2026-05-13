using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
	public class Plan:ModelBase
	{
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int DurationDays { get; set; }
		public decimal Price { get; set; }
		public bool isActive { get; set; }
		public virtual ICollection<MemberShip> PlanMembers { get; set; }= null!;
	}
}

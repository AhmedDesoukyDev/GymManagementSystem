using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
	public class MemberShip:ModelBase
	{
		public virtual Member Member { get; set; } = null!;
		public int MemberId { get; set; }
		public virtual Plan Plan { get; set; } = null!;
		public int PlanId { get; set; }
		//StartDate = CreatedAt
		public DateTime EndDate { get; set; }
		public string Status
		{
			get
			{
				if(EndDate <= DateTime.Now)
				{
					return "Expired";
				}
				else
				{
					return "Active";
				}
			}
		}
	}
}

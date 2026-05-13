using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
	//Needed because of attributes on the relationship
	//M - M -- will be 1 -> M and 1 ->M
	public class MemberSessions:ModelBase
	{
		public int MemberId { get; set; }
		public virtual Member Member { get; set; } = null!;
		public int SessionId { get; set; }
		public virtual Session Session { get; set; } = null!;
		//BookingDate = Created At from modelbase
		public bool IsAttended { get; set; } 
	}
}

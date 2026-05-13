using GymSystemDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace GymSystemDAL.Models
{

	public class Trainer : GymUser
	{
		public Specialties Specialties { get; set; }
		//HireDate ==CreatedAt from Modelbase
		public virtual ICollection<Session> Sessions { get; set; } = null!;
	}
}

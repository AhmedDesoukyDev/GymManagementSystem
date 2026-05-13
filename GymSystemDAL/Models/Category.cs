using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
	public class Category:ModelBase
	{
		public string Name { get; set; }
		public virtual ICollection<Session> Sessions { get; set; } = null!;
	}
}

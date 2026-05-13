using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{
	public class Session :ModelBase
	{
		public string Description { get; set; }
		public int Capacity { get; set; }
		//StartDate != Created At
		//Start is different to create
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public virtual ICollection<MemberSessions> MemberSessions { get; set; }= null!;

		#region Trainer-Session
		public int TrainerId { get; set; }
		public virtual Trainer SessionTrainer { get; set; } = null!;
		#endregion

		#region Session-Category
		public virtual int CategoryId { get; set; }

		public virtual Category SessionCategory { get; set; } = null!; 
		#endregion
	}
}

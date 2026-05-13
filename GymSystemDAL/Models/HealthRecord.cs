using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Models
{	//1 - 1 Relationship with member -- Shared Key
	public class HealthRecord : ModelBase// it will be with member because its 1:1 Relationship
	{
		public decimal Height { get; set; }
		public decimal Weight { get; set; }

		//Can be enum
		public string BloodType { get; set; }
		public string? Notes { get; set; }
		//in 1 - 1 Relationship ---> one nav property is enough
		//LastUpdate == UpdatedAt

	}
}

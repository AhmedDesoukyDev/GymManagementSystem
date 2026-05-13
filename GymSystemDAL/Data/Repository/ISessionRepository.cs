using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Repository
{
	public interface ISessionRepository:IGenericRepository<Session>
	{
		IEnumerable<Session> GetSessionsWithTrainersAndCategories();
		Session? GetSessionWithTrainerAndCategory(int id);

		int CountOfBookedSlots(int sessionId);

	}
}

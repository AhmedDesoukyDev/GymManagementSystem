using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Repository
{
	public class SessionRepository : GenericRepository<Session>, ISessionRepository
	{
		private readonly GymDbContext _dbContext;

		public SessionRepository(GymDbContext dbContext):base(dbContext)
		{
			_dbContext = dbContext;
		}
		public int CountOfBookedSlots(int sessionId)
		{
			var Booked = _dbContext.MemberSessions.Count(X=>X.Id == sessionId);
			return Booked;	
		}

		public IEnumerable<Session> GetSessionsWithTrainersAndCategories()
		{
			var result = _dbContext.Sessions.Include(X => X.SessionTrainer).
											Include(X => X.SessionCategory).ToList();

			return result;
		}

		public Session? GetSessionWithTrainerAndCategory(int id)
		{
			var result = _dbContext.Sessions.Include(X=>X.SessionTrainer).Include(X=>X.SessionCategory)
											.FirstOrDefault(X=>X.Id==id);
			return result;
		}
	}
}

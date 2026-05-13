using GymSystemDAL.Data.Repository;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.UnitOfWork
{
	public interface IUnitOfWork 
	{
		ISessionRepository SessionRepository { get; }
		IGenericRepository<T> GetRepository<T>() where T : ModelBase, new();
		int Complete();

		
	}
}

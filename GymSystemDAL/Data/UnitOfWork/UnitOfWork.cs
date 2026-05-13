using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Data.Repository;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork //Simulation of dbcontext
	{
		private readonly GymDbContext _dbContext;
		private readonly Dictionary<Type,object> Repositories = new ();
		public UnitOfWork(GymDbContext dbContext, ISessionRepository sessionRepository)
		{

			_dbContext = dbContext;
			SessionRepository = sessionRepository;
		}

		//Save changes will be via unitofwork onlyyy
		public int Complete() => _dbContext.SaveChanges(); //No need for dispose , clr will handle that

		public ISessionRepository SessionRepository { get;}
		//Make sure it wont create repository every time i need it
		public IGenericRepository<T> GetRepository<T>() where T : ModelBase, new()
		{
			var EntityType = typeof(T);
			if (Repositories.TryGetValue(EntityType, out var repository))
				return (IGenericRepository<T>)repository;
			var newRepository = new GenericRepository<T>(_dbContext);

			Repositories[EntityType] =  newRepository;
			return newRepository;
		}
	}
}

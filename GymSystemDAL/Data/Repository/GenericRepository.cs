using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase,new()
	{
		private readonly GymDbContext _dbContext;

		public GenericRepository(GymDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		//Expression make the delegate translate to query not compiled
		//"I'm describing what I want, not how to execute it."
		//Without it , filteration will happen in memory not in db because EF can deal with syntax tree
		public IEnumerable<T> GetAll(Expression<Func<T,bool>>? condition = null, bool asWithNoTracking = true)
		{
			IQueryable<T> result;
			if (asWithNoTracking)
				result = _dbContext.Set<T>().AsNoTracking();
			else
				result = _dbContext.Set<T>();

			if (condition is null)

				return  result.ToList();
			return  result.Where(condition).ToList();
 
			
		}

		public T? GetById(int id) =>  _dbContext.Set<T>().Find(id); //Find local then remote
		public void Add(T entity) => _dbContext.Set<T>().Add(entity); //Added State

		public void Delete(T entity)=> _dbContext.Set<T>().Remove(entity);

		public void Update(T entity)=> _dbContext.Set<T>().Update(entity);
	}
}

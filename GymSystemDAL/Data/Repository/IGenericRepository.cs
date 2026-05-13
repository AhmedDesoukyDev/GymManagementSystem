using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymSystemDAL.Data.Repository
{
	public interface IGenericRepository<T> where T : ModelBase,new() //Concrete class
	{
		IEnumerable<T>GetAll(Expression<Func<T, bool>>? condition = null, bool asWithNoTracking = true);
		T? GetById(int id);

		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);

	}
}

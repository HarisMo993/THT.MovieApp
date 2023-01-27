using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace THT.MovieApp.Data.Interfaces.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null);
        Task<T> GetById(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}

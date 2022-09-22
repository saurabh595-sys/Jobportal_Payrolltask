using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jobportel.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);

        Task<T> Add(T entity);

        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);

        Task<List<T>> GetDefaultList(Expression<Func<T, bool>> expression);
    }
}

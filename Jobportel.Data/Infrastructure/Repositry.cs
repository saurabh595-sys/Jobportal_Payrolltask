using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jobportel.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Context _contex;
        public Repository(Context contex)
        {
            _contex = contex;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _contex.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _contex.Set<T>().FindAsync(id);
        }
        public async Task<T> Add(T entity)
        {
            await _contex.AddAsync<T>(entity);
            await _contex.SaveChangesAsync();
            return entity;
        }
        public async Task Update(T entity)
        {
            _contex.Entry(entity).State = EntityState.Modified;
            _contex.Set<T>().Update(entity);
            await _contex.SaveChangesAsync();
        }

       
        public async Task Delete(T entity)
        {
            _contex.Set<T>().Remove(entity);
            await _contex.SaveChangesAsync();
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await _contex.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetDefaultList(Expression<Func<T, bool>> expression)
        {
            return await _contex.Set<T>().Where(expression).ToListAsync();
        }
    }
}

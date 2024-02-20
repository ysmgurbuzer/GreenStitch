using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> :IGenericDal<T>
        
        where T : class
    {
        private readonly GreenContext _context;

        public GenericRepository(GreenContext context)
        {
             _context = context;
        }
        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _context.Set<T>().Where(filter).ToList();
            }
            else
            {
                return _context.Set<T>().ToList();
            }
        }
        public void Delete(T t) 
        {
          _context.Set<T>().Remove(t);
        }    

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id).AsTask();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();    
        }
        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);

        }
        public async Task AddAsync(T t)
        {
             await _context.Set<T>().AddAsync(t);
        }
     
        public void Update(T t, T unchanged)
        {
            _context.Entry(unchanged).CurrentValues.SetValues(t);

        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

    }
}

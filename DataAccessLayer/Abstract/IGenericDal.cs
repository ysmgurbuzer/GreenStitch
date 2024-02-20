using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> 
        where T : class
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        void Delete(T t);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetListAsync();
        Task AddAsync(T t);
        void Update(T t, T unchanged);

        Task<T> FindAsync(object id);
        IQueryable<T> GetQuery();
    }
}

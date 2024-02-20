using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class Uow:IUow
    {
        private readonly GreenContext _context;
        public Uow(GreenContext context)
        {
             _context = context;
        }

        public IGenericDal<T> GetGenericDal<T>() where T : class 
        {
            return new GenericRepository<T>(_context);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}




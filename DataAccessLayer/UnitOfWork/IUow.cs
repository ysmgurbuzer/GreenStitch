using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUow
    {
        IGenericDal<T> GetGenericDal<T>() where T : class;
        Task SaveChangeAsync();
    }
}

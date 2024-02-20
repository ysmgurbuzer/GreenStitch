using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfOrderHistoryRepository : GenericRepository<OrderHistory>, IOrderHistoryDal
    {
        public EfOrderHistoryRepository(GreenContext context) : base(context)
        {

        }
    
    }
}

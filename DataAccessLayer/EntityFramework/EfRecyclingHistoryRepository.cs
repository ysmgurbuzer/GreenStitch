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
    public class EfRecyclingHistoryRepository : GenericRepository<RecyclingHistory>, IRecyclingHistoryDal
    {
        public EfRecyclingHistoryRepository(GreenContext context) : base(context)
        {

        }
    
    }
}

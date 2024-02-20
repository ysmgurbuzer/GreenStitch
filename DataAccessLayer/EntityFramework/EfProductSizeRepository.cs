using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfProductSizeRepository : GenericRepository<ProductSize>, IProductSizeDal
    {
        private readonly GreenContext _context;
        public EfProductSizeRepository(GreenContext context) : base(context)
        {

        }
        public async Task<ProductSize> GetByProductIdAsync(int productId)
        {
            return await GetQuery().FirstOrDefaultAsync(ps => ps.ProductId == productId);
        }

    }
}
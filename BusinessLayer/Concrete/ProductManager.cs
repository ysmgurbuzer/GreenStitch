using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.UnitOfWork;
using Dtos;
using Dtos.ProductDtos;
using EntityLayer;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : GenericManager<ProductCreateDto, ProductUpdateDto, ProductListDto, Product>, IProductService
    {
        private readonly GreenContext _context;

        public ProductManager(IMapper mapper,
            IValidator<ProductCreateDto> createDto,
            IValidator<ProductUpdateDto> listDto,
            IValidator<ProductListDto> updateDto,
            IUow uow, GreenContext context) : base(mapper, createDto, updateDto, listDto, uow)
        {
            _context = context;
        }

        public async Task<List<Product>> GetFilteredProductsAsync(ProductFilterModelDto filters)
        {
            var query = _context.Products
                .Include(p => p.Advertisement)
                .Include(p => p.Sizes) 
                .AsQueryable();

            if (filters.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filters.MinPrice.Value);
            }

            if (filters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filters.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(filters.Color))
            {
                query = query.Where(p => p.Color == filters.Color);
            }

            if (filters.CategoryId.HasValue)
            {
                query = query.Where(p => p.Advertisement.CategoryId == filters.CategoryId.Value);
            }

            if (filters.FabricId.HasValue)
            {
                query = query.Where(p => p.FabricId == filters.FabricId.Value);
            }

           

            return await query.ToListAsync();
        }
    }
}

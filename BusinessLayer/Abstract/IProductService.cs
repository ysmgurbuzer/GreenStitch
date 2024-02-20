using Dtos;
using Dtos.ProductDtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProductService:IGenericService<ProductCreateDto, ProductUpdateDto,ProductListDto,Product>
    {
        Task<List<Product>> GetFilteredProductsAsync(ProductFilterModelDto filters);
    }
}

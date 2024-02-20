using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using Dtos;
using EntityLayer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductSizeManager : GenericManager<ProductSizeCreateDto, ProductSizeUpdateDto, ProductSizeListDto, ProductSize>, IProductSizeService
    {
        public ProductSizeManager(IMapper mapper,
            IValidator<ProductSizeCreateDto> createDto,
            IValidator<ProductSizeListDto> listDto,
            IValidator<ProductSizeUpdateDto> updateDto,
            IUow uow) : base(mapper, createDto, listDto, updateDto, uow)
        {
        }

    }
}

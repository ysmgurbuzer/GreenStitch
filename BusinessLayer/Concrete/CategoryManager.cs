using AutoMapper;
using BusinessLayer.Abstract;
using CommonLayer.CustomErrorHandler;
using DataAccessLayer.UnitOfWork;
using Dtos;
using EntityLayer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLayer.Concrete
{
    public class CategoryManager:GenericManager<CategoryCreateDto,CategoryUpdateDto,CategoryListDto,Category>,ICategoryService
    {
        private readonly ICategoryService _categoryService;
        private readonly IAdvertisementService _advertService;   
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public CategoryManager(IMapper mapper, 
            IValidator<CategoryCreateDto> createDto,
             IValidator<CategoryUpdateDto> updateDto,
            IValidator<CategoryListDto> listDto, 
           
            IUow uow, IAdvertisementService advertService,IProductService productService

            ) : base(mapper, createDto, listDto, updateDto, uow)
        {

            _advertService = advertService;
            _productService = productService;
     


        }

    


    

    }
}

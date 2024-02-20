using AutoMapper;
using CommonLayer.CustomErrorHandler;
using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapps
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
                CreateMap<ProductCreateDto,Product>().ReverseMap();
                CreateMap<ProductListDto, Product>().ReverseMap();
                CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<Response<Product>, Advertisement>().ReverseMap();
        }
    }
}

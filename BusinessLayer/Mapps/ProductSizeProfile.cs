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
    public class ProductSizeProfile:Profile
    {
        public ProductSizeProfile()
        {
            CreateMap<ProductSizeCreateDto, ProductSize>().ReverseMap();
            CreateMap<ProductSizeListDto, ProductSize>().ReverseMap();
            CreateMap<ProductSizeUpdateDto, ProductSize>().ReverseMap();
            CreateMap<Response<ProductSize>, ProductSize>().ReverseMap();
            CreateMap<Response<ProductSizeListDto>, ProductSize>().ReverseMap();
        }
    }
}

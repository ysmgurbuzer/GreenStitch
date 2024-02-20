using AutoMapper;
using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapps
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
                CreateMap<CategoryCreateDto,Category>().ReverseMap();
                CreateMap<CategoryListDto, Category>().ReverseMap();
                CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }
    }
}

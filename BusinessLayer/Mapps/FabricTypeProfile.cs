using AutoMapper;
using Azure;
using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapps
{
    public class FabricTypeProfile:Profile
    {
        public FabricTypeProfile()
        {
                CreateMap<FabricTypeCreateDto,FabricType>().ReverseMap();
                CreateMap<FabricTypeListDto, FabricType>().ReverseMap();
                CreateMap<FabricTypeUpdateDto, FabricType>().ReverseMap();
            CreateMap<Response<FabricTypeListDto>, FabricType>().ReverseMap();
        }
    }
}

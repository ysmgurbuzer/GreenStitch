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
    public class AdvertisementProfile:Profile
    {
        public AdvertisementProfile()
        {
                CreateMap<AdvertisementCreateDto,Advertisement>().ReverseMap();
                CreateMap<AdvertisementListDto, Advertisement>().ReverseMap();
                CreateMap<AdvertisementUpdateDto, Advertisement>().ReverseMap();
            CreateMap<Response<Advertisement>, Advertisement>().ReverseMap();
            CreateMap<Response<AdvertisementListDto>, Advertisement>().ReverseMap();
            CreateMap<AdvertisementListDto, List<Advertisement>>().ReverseMap();
      

        }
    }
}
